import { Component, HostListener, OnInit } from '@angular/core';
import { ProductModel } from '../models/ProductModel';
import { ProductService } from '../services/product.service';
import { Product } from '../product/product';
import { CartItem } from '../models/CartItem';
import { FormsModule } from '@angular/forms';
import { debounceTime, distinctUntilChanged, Subject, switchMap, tap } from 'rxjs';


@Component({
  selector: 'app-products',
  imports: [Product, FormsModule],
  templateUrl: './products.html',
  styleUrl: './products.css'
})
export class Products implements OnInit {
  products: ProductModel[] = [];
  cartItems: CartItem[] = [];
  cartCount: number = 0;
  searchString: string = "";
  loading: boolean = true;
  searchSubject = new Subject<string>();
  limit:number = 10;
  skip:number = 0;
  total:number = 0;
  constructor(private productService: ProductService) {

  }


  handleSearchProducts() {
    this.searchSubject.next(this.searchString);
  }

  handleAddToCart(event: number) {
    console.log("Handling add to cart - " + event)
    let flag = false;
    for (let i = 0; i < this.cartItems.length; i++) {
      if (this.cartItems[i].Id == event) {
        this.cartItems[i].Count++;
        flag = true;
      }
    }
    if (!flag)
      this.cartItems.push(new CartItem(event, 1));
    this.cartCount++;
  }
  ngOnInit(): void {
    this.searchSubject.pipe(
      debounceTime(500),
      distinctUntilChanged(),
      tap(() => this.loading = true),
      switchMap(query => this.productService.getProductSearchResult(query, this.limit, this.skip)),
      tap(() => this.loading = false)).subscribe({
        next: (data: any) => { 
          this.products = data.products as ProductModel[]; 
          this.total = data.total;
          console.log(this.total);
        }
      });
    this.productService.getAllProducts().subscribe(
      {
        next: (data: any) => {
          this.products = data.products as ProductModel[];
        },
        error: (err) => { },
        complete: () => { }
      }
    )
  }
  @HostListener('window:scroll',[])
  onScroll():void
  {

    const scrollPosition = window.innerHeight + window.scrollY;
    const threshold = document.body.offsetHeight-100;
    if(scrollPosition>=threshold && this.products?.length<this.total)
    {
      console.log(scrollPosition);
      console.log(threshold)
      
      this.loadMore();
    }
  }
  loadMore(){
    this.loading = true;
    this.skip += this.limit;
    this.productService.getProductSearchResult(this.searchString,this.limit,this.skip)
        .subscribe({
          next:(data:any)=>{
            [...this.products,...data.products]
            this.loading = false;
          }
        })
  }
}