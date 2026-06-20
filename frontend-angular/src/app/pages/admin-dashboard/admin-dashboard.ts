import { Component, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { Product } from '../../core/models/product.model';
import { ProductService } from '../../core/services/product.service';

@Component({
  selector: 'app-admin-dashboard',
  imports: [FormsModule],
  templateUrl: './admin-dashboard.html',
  styleUrl: './admin-dashboard.css',
})
export class AdminDashboard {
  private readonly productService = inject(ProductService);

  products = signal<Product[]>([]);

  formProduct: Product = {
    idProduit: 0,
    nom: '',
    description: '',
    prixUnitaire: 0,
    stockOnHand: 0,
    actif: true
  };

  constructor() {
    this.loadProducts();
  }

  loadProducts(): void {
    this.productService.getProducts().subscribe({
      next: (data) => this.products.set(data),
      error: (error) => console.error('Erreur chargement produits', error)
    });
  }

  saveProduct(): void {
  const productToSave = { ...this.formProduct };

  if (productToSave.idProduit === 0) {
    this.productService.createProduct(productToSave).subscribe(() => {
      window.location.reload();
    });
  } else {
    this.productService.updateProduct(productToSave).subscribe(() => {
      window.location.reload();
    });
  }
}

  editProduct(product: Product): void {
    this.formProduct = { ...product };
  }

  deleteProduct(id: number): void {
    this.productService.deleteProduct(id).subscribe(() => {
      this.loadProducts();
    });
  }

  resetForm(): void {
    this.formProduct = {
      idProduit: 0,
      nom: '',
      description: '',
      prixUnitaire: 0,
      stockOnHand: 0,
      actif: true
    };
  }
}