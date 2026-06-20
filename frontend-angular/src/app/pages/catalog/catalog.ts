import { Component, inject, signal } from '@angular/core';
import { RouterLink } from '@angular/router';

import { Product } from '../../core/models/product.model';
import { ProductService } from '../../core/services/product.service';
import { CartService } from '../../core/services/cart.service';

@Component({
  selector: 'app-catalog',
  imports: [RouterLink],
  templateUrl: './catalog.html',
  styleUrl: './catalog.css'
})
export class Catalog {
  private readonly productService = inject(ProductService);
  private readonly cartService = inject(CartService);

  products = signal<Product[]>([]);
  isLoading = signal(true);
  notification = signal('');

  constructor() {
    this.productService.getProducts().subscribe({
      next: (data) => {
        this.products.set(data);
        this.isLoading.set(false);
      },
      error: (error) => {
        console.error('Erreur lors du chargement des produits', error);
        this.isLoading.set(false);
      }
    });
  }

  addToCart(product: Product): void {
    this.cartService.addToCart(product);
    this.notification.set(`${product.nom} ajouté au panier`);

    setTimeout(() => {
      this.notification.set('');
    }, 1500);
  }
}