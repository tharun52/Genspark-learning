import { Component, HostListener } from '@angular/core';
import { ProductModel } from '../models/ProductModel';
import { debounceTime, distinctUntilChanged, Subject, switchMap, tap } from 'rxjs';
import { ProductService } from '../services/product.service';
import { Product } from "../product/product";
import { FormsModule } from '@angular/forms';
@Component({
  selector: 'app-home',
  imports: [Product,FormsModule],
  templateUrl: './home.html',
  styleUrl: './home.css'
})
export class Home {
  products: ProductModel[] = [];
  searchString: string = "";
  searchSubject = new Subject<string>();
  loading: boolean = false;
  limit = 10;
  skip = 0;
  total = 0;
  constructor(private productService: ProductService) {

  }
  handleSearchProducts() {
    this.searchSubject.next(this.searchString);
  }
  ngOnInit(): void {
    this.searchSubject.pipe(
      debounceTime(400),
      distinctUntilChanged(),
      tap(() => this.loading = true),
      switchMap(query => this.productService.getProductSearchResult(query, this.limit, this.skip)),
      tap(() => this.loading = false)).subscribe({
        next: (data: any) => {
          this.products = data.products as ProductModel[];
          this.total = data.total;
          console.log(this.total)
        }
      });
  }

  @HostListener('window:scroll', [])
  onScroll(): void {

    const scrollPosition = window.innerHeight + window.scrollY;
    const threshold = document.body.offsetHeight - 100;
    if (scrollPosition >= threshold && this.products?.length < this.total) {
      console.log("Scroll : "+scrollPosition);
      console.log("threshold: "+threshold)
      
      this.loadMore();
    }
  }
  loadMore() {
  this.loading = true;
  this.skip += this.limit;

  const currentScroll = window.scrollY;

  this.productService.getProductSearchResult(this.searchString, this.limit, this.skip)
    .subscribe({
      next: (data: any) => {
        this.products = [...this.products, ...data.products];

        this.loading = false;

        
        setTimeout(() => {
          window.scrollTo({
            top: currentScroll + 100,
            behavior: 'smooth'
          });
        }, 100); 
      }
    });
  }
}

