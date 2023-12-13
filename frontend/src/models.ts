export class Product {
  productId?: number;
  productNumber?: number;
  productName?: string;
  pricePrKilo?: number;
  productType?: string;
  countryOfBirth?: string;
  productionCountry?: string;
  description?: string;
  imgUrl?: string;
  minExpDate?: Date;
}

export class Customer {
  customerId?: number;
  firstName?: string;
  lastName?: string;
  email?: string;
  address?: string;
  zip?: number;
  city?: string;
  country?: string;
  phone?: number;
}
export class ResponseDto<T> {
  responseData?: T;
  messageToClient?: string;
}
