import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { AuthService } from '../../core/services/auth.service';
import { CartService } from '../../core/services/cart.service';
import { OrderService } from '../../core/services/order.service';
import { CreateOrderRequest } from '../../core/models/order.model';

@Component({
  selector: 'app-cart',
  imports: [FormsModule],
  templateUrl: './cart.html',
  styleUrl: './cart.css',
})
export class Cart {
  cartService = inject(CartService);

  private readonly authService = inject(AuthService);
  private readonly orderService = inject(OrderService);

  rue = '';
  complement = '';
  localite = '';
  codePostal = '';
  contactNom = '';
  contactTel = '';

  errorMessage = '';
  successMessage = '';

  removeItem(productId: number): void {
    this.cartService.removeFromCart(productId);
  }

  clearCart(): void {
    this.cartService.clearCart();
  }

  createOrder(): void {
    this.errorMessage = '';
    this.successMessage = '';

    const user = this.authService.currentUser();

    if (!user) {
      this.errorMessage = 'Vous devez être connecté pour passer une commande.';
      return;
    }

    if (this.cartService.items().length === 0) {
      this.errorMessage = 'Votre panier est vide.';
      return;
    }

    if (
      !this.rue.trim() ||
      !this.localite.trim() ||
      !this.codePostal.trim()
    ) {
      this.errorMessage =
        'La rue, la ville et le code postal sont obligatoires.';
      return;
    }

    const request: CreateOrderRequest = {
      clientEmail: user.email,
      rue: this.rue,
      complement: this.complement || null,
      localite: this.localite,
      codePostal: this.codePostal,
      contactNom:
        this.contactNom ||
        `${user.prenom ?? ''} ${user.nom}`.trim(),
      contactTel:
        this.contactTel ||
        user.telephone ||
        null,
      totalTtc: this.cartService.total(),
      lignes: this.cartService.items().map(item => ({
        idProduit: item.product.idProduit,
        nom: item.product.nom,
        prixUnitaire: item.product.prixUnitaire,
        quantite: item.quantity
      }))
    };

    this.orderService.createOrder(request).subscribe({
      next: (response) => {
        this.successMessage =
          `Commande n°${response.idCommande} créée avec succès.`;

        this.cartService.clearCart();

        this.rue = '';
        this.complement = '';
        this.localite = '';
        this.codePostal = '';
        this.contactNom = '';
        this.contactTel = '';
      },

      error: (error) => {
        this.errorMessage =
          typeof error.error === 'string'
            ? error.error
            : 'Impossible de créer la commande.';
      }
    });
  }
}