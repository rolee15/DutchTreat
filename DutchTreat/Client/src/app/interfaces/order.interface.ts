export interface Order {
  orderId: number;
  orderDate: Date;
  orderNumber: string;
  items: OrderItem[];
}

export interface OrderItem {
  id: number;
  quantity: number;
  unitPrice: number;
  productId: number;
  productCategory: string;
  productSize: string;
  productTitle: string;
  productArtist: string;
  productArtId: string;
}