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
  minExpDate?: number;
}

export class User {
  userId?: number;
  firstName?: string;
  lastName?: string;
  email?: string;
  address?: string;
  zip?: number;
  city?: string;
  country?: string;
  phone?: number;
}


export class ProductType {
  typeId?: number;
  typeName?: string;
}

export class CartItem {
  productId?: number;
  productName?: string;
  productImgUrl?: string;
  pricePrKilo?: number;
  quantity?: number;
}
export class ResponseDto<T> {
  responseData?: T;
  messageToClient?: string;
}
