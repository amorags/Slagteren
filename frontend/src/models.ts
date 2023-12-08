export class Product {
  product_id?: number;
  product_number?: number;
  product_name?: string;
  price_per_kilo?: number;
  description?: string;
  product_type?: string;
  product_img?: string;
  country_of_birth?: string;
  country_of_slaughter?: string;
  Expiration_date?: number;
}

export class ResponseDto<T> {
  responseData?: T;
  messageToClient?: string;
}
