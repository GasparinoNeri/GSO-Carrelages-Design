import { Injectable, computed, signal } from '@angular/core';
import { CartItem } from '../models/cart-item.model';
import { Product } from '../models/product.model';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  items = signal<CartItem[]>([]);

  total = computed(() =>
    this.items().reduce(
      (sum, item) => sum + item.product.prixUnitaire * item.quantity,
      0
    )
  );

  addToCart(product: Product): void {
    this.items.update(items => {
      const existingItem = items.find(
        item => item.product.idProduit === product.idProduit
      );

      if (existingItem) {
        return items.map(item =>
          item.product.idProduit === product.idProduit
            ? { ...item, quantity: item.quantity + 1 }
            : item
        );
      }

      return [...items, { product, quantity: 1 }];
    });
  }

  removeFromCart(productId: number): void {
    this.items.update(items =>
      items
        .map(item =>
          item.product.idProduit === productId
            ? { ...item, quantity: item.quantity - 1 }
            : item
        )
        .filter(item => item.quantity > 0)
    );
  }

  clearCart(): void {
    this.items.set([]);
  }
}