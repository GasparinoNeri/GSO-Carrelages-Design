export interface User {
  idUtilisateur: number;
  nom: string;
  prenom: string | null;
  email: string;
  telephone: string | null;
  adresse: string | null;
  dateNaissance: string | null;
  photoProfil: string | null;
  role: string;
  actif: boolean;
  createdAt?: string;
}

export interface RegisterRequest {
  nom: string;
  prenom: string | null;
  email: string;
  telephone: string | null;
  adresse: string | null;
  dateNaissance: string | null;
  photoProfil: string | null;
  password: string;
}
