import { Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { RegisterRequest, User } from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly apiUrl = 'http://localhost:5071/api/Auth';

  currentUser = signal<User | null>(this.getStoredUser());

  constructor(private readonly http: HttpClient) {}

  login(email: string, password: string): Observable<User> {
    return this.http.post<User>(`${this.apiUrl}/login`, {
      email,
      password
    }).pipe(
      tap((user) => {
        this.saveUser(user);
      })
    );
  }

  register(request: RegisterRequest): Observable<User> {
    return this.http.post<User>(
      `${this.apiUrl}/register`,
      request
    ).pipe(
      tap((user) => {
        this.saveUser(user);
      })
    );
  }

  logout(): void {
    this.currentUser.set(null);
    localStorage.removeItem('currentUser');
  }

  isAdmin(): boolean {
    return this.currentUser()?.role === 'admin';
  }

  isLoggedIn(): boolean {
    return this.currentUser() !== null;
  }

  getProfile(id: number): Observable<User> {
    return this.http.get<User>(`${this.apiUrl}/profile/${id}`);
  }

  updateProfile(
    id: number,
    request: {
      nom: string;
      prenom: string | null;
      telephone: string | null;
      adresse: string | null;
      dateNaissance: string | null;
      photoProfil: string | null;
    }
  ): Observable<User> {
    return this.http.put<User>(
      `${this.apiUrl}/profile/${id}`,
      request
    ).pipe(
      tap((user) => {
        this.saveUser(user);
      })
    );
  }

  private saveUser(user: User): void {
    this.currentUser.set(user);
    localStorage.setItem(
      'currentUser',
      JSON.stringify(user)
    );
  }

  private getStoredUser(): User | null {
    const storedUser = localStorage.getItem('currentUser');

    if (!storedUser) {
      return null;
    }

    try {
      return JSON.parse(storedUser) as User;
    } catch {
      localStorage.removeItem('currentUser');
      return null;
    }
  }
}
