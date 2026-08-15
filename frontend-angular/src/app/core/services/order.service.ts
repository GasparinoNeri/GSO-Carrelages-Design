import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import {
  CreateOrderRequest,
  Order
} from '../models/order.model';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  private readonly apiUrl = 'http://localhost:5071/api/Orders';

  constructor(private readonly http: HttpClient) {}

  createOrder(
    request: CreateOrderRequest
  ): Observable<{ idCommande: number }> {
    return this.http.post<{ idCommande: number }>(
      this.apiUrl,
      request
    );
  }

  getClientOrders(email: string): Observable<Order[]> {
    return this.http.get<Order[]>(
      `${this.apiUrl}/client/${encodeURIComponent(email)}`
    );
  }

  getAllOrders(): Observable<Order[]> {
    return this.http.get<Order[]>(this.apiUrl);
  }

  updateStatus(
    idCommande: number,
    statut: string
  ): Observable<void> {
    return this.http.put<void>(
      `${this.apiUrl}/${idCommande}/status`,
      { statut }
    );
  }
}
