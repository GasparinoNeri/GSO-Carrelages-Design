export interface OrderLine {
  idProduit: number;
  nom: string;
  prixUnitaire: number;
  quantite: number;
}

export interface CreateOrderRequest {
  clientEmail: string;
  rue: string;
  complement: string | null;
  localite: string;
  codePostal: string;
  contactNom: string | null;
  contactTel: string | null;
  totalTtc: number;
  lignes: OrderLine[];
}

export interface Order {
  idCommande: number;
  idClient: number;
  clientEmail: string;
  statut: string;
  totalTtc: number;
  devise: string;
  lignes: OrderLine[];
  createdAt: string;
}
