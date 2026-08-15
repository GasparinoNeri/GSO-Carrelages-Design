import { Component, inject, OnInit, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

import { AuthService } from '../../core/services/auth.service';
import { OrderService } from '../../core/services/order.service';
import { Order } from '../../core/models/order.model';

@Component({
  selector: 'app-profile',
  imports: [FormsModule],
  templateUrl: './profile.html',
  styleUrl: './profile.css',
})
export class Profile implements OnInit {
  private readonly authService = inject(AuthService);
  private readonly orderService = inject(OrderService);
  private readonly router = inject(Router);

  user = this.authService.currentUser;

  activeTab: 'informations' | 'commandes' | 'parametres' = 'informations';

  orders = signal<Order[]>([]);
  ordersLoading = signal(false);
  ordersError = '';

  isEditing = false;
  errorMessage = '';
  successMessage = '';

  nom = '';
  prenom = '';
  telephone = '';
  adresse = '';
  dateNaissance = '';
  photoProfil = '';

  ngOnInit(): void {
    this.loadOrders();
  }

  loadOrders(): void {
    const currentUser = this.user();

    if (!currentUser) {
      return;
    }

    this.ordersLoading.set(true);
    this.ordersError = '';

    this.orderService
      .getClientOrders(currentUser.email)
      .subscribe({
        next: (orders) => {
          this.orders.set(orders);
          this.ordersLoading.set(false);
        },
        error: () => {
          this.ordersError =
            'Impossible de charger vos commandes.';
          this.ordersLoading.set(false);
        }
      });
  }

  setActiveTab(tab: 'informations' | 'commandes' | 'parametres'): void {
      this.activeTab = tab;
  }

  startEditing(): void {
    const currentUser = this.user();

    if (!currentUser) {
      return;
    }

    this.nom = currentUser.nom;
    this.prenom = currentUser.prenom ?? '';
    this.telephone = currentUser.telephone ?? '';
    this.adresse = currentUser.adresse ?? '';

    this.dateNaissance = currentUser.dateNaissance
      ? currentUser.dateNaissance.substring(0, 10)
      : '';

    this.photoProfil = currentUser.photoProfil ?? '';

    this.errorMessage = '';
    this.successMessage = '';
    this.isEditing = true;
  }

  cancelEditing(): void {
    this.isEditing = false;
    this.errorMessage = '';
  }

  saveProfile(): void {
    const currentUser = this.user();

    if (!currentUser) {
      return;
    }

    this.authService.updateProfile(
      currentUser.idUtilisateur,
      {
        nom: this.nom,
        prenom: this.prenom || null,
        telephone: this.telephone || null,
        adresse: this.adresse || null,
        dateNaissance: this.dateNaissance || null,
        photoProfil: this.photoProfil || null
      }
    ).subscribe({
      next: () => {
        this.isEditing = false;
        this.successMessage =
          'Profil modifié avec succès.';
      },
      error: (error) => {
        this.errorMessage =
          typeof error.error === 'string'
            ? error.error
            : 'Impossible de modifier le profil.';
      }
    });
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
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