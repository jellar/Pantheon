import {Component, OnInit} from "@angular/core";
import {ActivatedRoute, Router} from "@angular/router";
import {AbstractControlOptions, FormBuilder, FormGroup, Validators} from "@angular/forms";
import {CurrencyPipe} from "@angular/common";
import {AccountDetails, Transaction} from "../account/account.model";
import {RxwebValidators} from "@rxweb/reactive-form-validators";
import {AccountService} from "../_services/account.service";

@Component({
  templateUrl: './transaction.component.html'
})

export class TransactionComponent implements OnInit{
  accountDetails: AccountDetails;
  id!:string;
  form!: FormGroup;
  transactionType: any = ["Deposit", "Withdraw"];
  currencies: any = ['GBP', 'USD'];
  loading = false;
  submitted = false;
  transaction: Transaction = new Transaction();
  errors: string[];
  constructor(private route: ActivatedRoute, private router: Router,
              private formBuilder: FormBuilder, private accountService: AccountService) {
  }

  ngOnInit() {
    this.id  = this.route.snapshot.params['id'];

    this.accountService.getAccountDetails().subscribe(result=> {
      this.accountDetails = result;
    }, error=> console.error(error));


    const formOptions: AbstractControlOptions = {};
    this.form = this.formBuilder.group({
      accountId:[],
      reference: ['', Validators.required],
      amount: ['', RxwebValidators.numeric({allowDecimal:true,isFormat:true})],
      type: ['', Validators.required],
      currencyType: ['', Validators.required]
    }, formOptions);
  }

  // convenience getter for easy access to form fields
  get f() {
    return this.form.controls;
  }

  onSubmit() {
    this.submitted = true;
    // stop here if form is invalid
    if (this.form.invalid) {
      return;
    }

    this.loading = true;

    let amount: number = parseInt(this.form.value.amount);
    console.log(this.form.value.type);
    let type: number = this.form.value.type == 'Deposit' ? 0 : 1;
    let transaction = {...this.form.value, accountId: this.id, amount: amount, transactionType: type};
    this.accountService.postTransaction(transaction).subscribe(result=> {
      this.loading = false;
      this.router.navigate(['/']);
    }, error => console.error(error))

  }
}
