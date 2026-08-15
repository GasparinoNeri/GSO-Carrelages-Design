import { Component, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { Product } from '../../core/models/product.model';
import { Order } from '../../core/models/order.model';

import { ProductService } from '../../core/services/product.service';
import { OrderService } from '../../core/services/order.service';

@Component({
  selector: 'app-admin-dashboard',
  imports: [FormsModule],
  templateUrl: './admin-dashboard.html',
  styleUrl: './admin-dashboard.css',
})
export class AdminDashboard {
  private readonly productService = inject(ProductService);
  private readonly orderService = inject(OrderService);

  activeTab: 'produits' | 'commandes' = 'produits';

  products = signal<Product[]>([]);
  orders = signal<Order[]>([]);

  ordersLoading = signal(false);
  ordersError = '';

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
    this.loadOrders();
  }

  setActiveTab(tab: 'produits' | 'commandes'): void {
    this.activeTab = tab;
  }

  loadProducts(): void {
    this.productService.getProducts().subscribe({
      next: (data) => this.products.set(data),
      error: (error) =>
        console.error('Erreur chargement produits', error)
    });
  }

  saveProduct(): void {
    const productToSave = { ...this.formProduct };

    if (productToSave.idProduit === 0) {
      this.productService.createProduct(productToSave).subscribe(() => {
        this.loadProducts();
        this.resetForm();
      });
    } else {
      this.productService.updateProduct(productToSave).subscribe(() => {
        this.loadProducts();
        this.resetForm();
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

  loadOrders(): void {
    this.ordersLoading.set(true);
    this.ordersError = '';

    this.orderService.getAllOrders().subscribe({
      next: (orders) => {
        this.orders.set(orders);
        this.ordersLoading.set(false);
      },
      error: () => {
        this.ordersError =
          'Impossible de charger les commandes.';
        this.ordersLoading.set(false);
      }
    });
  }

  updateOrderStatus(
    idCommande: number,
    statut: string
  ): void {
    this.orderService
      .updateStatus(idCommande, statut)
      .subscribe({
        next: () => {
          this.loadOrders();
        },
        error: () => {
          this.ordersError =
            'Impossible de modifier le statut.';
        }
      });
  }

  getStatusLabel(status: string): string {
    switch (status) {
      case 'en_attente':
        return 'En attente';

      case 'payee':
        return 'Payée';

      case 'expediee':
        return 'Expédiée';

      case 'livree':
        return 'Livrée';

      case 'annulee':
        return 'Annulée';

      default:
        return status;
    }
  }
}