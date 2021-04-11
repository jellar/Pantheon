export class AccountDetails{
  id:string = "";
  name:string = "";
  balance: number;
  number: string;
}

export class Transaction{
  reference: string;
  dateOn: Date;
  amount: number;
  type: string;
}
