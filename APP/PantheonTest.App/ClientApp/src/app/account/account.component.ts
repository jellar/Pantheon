import {Component, OnInit} from "@angular/core";
import {AccountService} from "../_services/account.service";
import {AccountDetails, Transaction} from "./account.model";
import { downloadFile } from 'file-saver';
@Component({
  templateUrl: './account.component.html'
})

export class AccountComponent implements OnInit{
  accountDetails: AccountDetails;
  id: string;
  transactions: Transaction[]
  page = 1;
  count = 0;
  pageSize = 10;
  pageSizes = [10, 15];

  constructor(private accountService: AccountService) {
  }

  ngOnInit(){
    this.accountService.getAccountDetails().subscribe(result=> {
      this.accountDetails = result;
      this.id = result.id;
      this.retrieveTransactions();
    }, error=> console.error(error));
  }

  retrieveTransactions() {
    let id = this.id == undefined ? '' : this.id;
    this.accountService.getPagedTransactions(id, this.page, this.pageSize)
      .subscribe(
        response => {
          // @ts-ignore
          const {transactions, count} = response;
          this.transactions = transactions;
          this.count = count;
        },
        error => console.error(error));
  }

  handlePageChange(event) {
    this.page = event;
    this.retrieveTransactions();
  }

  handlePageSizeChange(event) {
    this.pageSize = event.target.value;
    this.page = 1;
    this.retrieveTransactions();
  }

  downloadCsv(){
    let id = this.id == undefined ? '' : this.id;
    this.accountService.getTransactionsToExport(id)
      .subscribe(
        (blob) => {
          const downloadURL = window.URL.createObjectURL(blob);
          const link = document.createElement('a');
          link.href = downloadURL;
          link.download = `${id}.csv`;
          link.click();
        },
        error => console.error(error));
  }
}

