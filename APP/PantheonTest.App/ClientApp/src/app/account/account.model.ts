export class AccountDetails{
  id:string = "";
  name:string = "";
  balance: number;
  number: string;
  currency: string;
}

export class Transaction{
  reference: string;
  dateOn: Date;
  amount: number;
  type: string;
  balance: number;
}
