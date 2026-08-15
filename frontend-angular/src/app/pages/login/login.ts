import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-login',
  imports: [FormsModule, RouterLink],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
  private readonly authService = inject(AuthService);
  private readonly router = inject(Router);

  email = '';
  password = '';
  errorMessage = '';

  onLogin(): void {
    this.errorMessage = '';

    this.authService.login(
      this.email,
      this.password
    ).subscribe({
        next: (user) => {
        if (user.role === 'admin') {
          this.router.navigate(['/admin']);
        } else {
          this.router.navigate(['/profile']);
        }
        },
        error: () => {
          this.errorMessage = 'Email ou mot de passe incorrect';
        }
    });
  }
}