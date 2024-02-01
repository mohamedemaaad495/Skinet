import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Product } from 'src/app/shared/models/product';
import { ShopService } from '../shop.service';
import { brand } from 'src/app/shared/models/type';
import { type } from 'src/app/shared/models/brand';
import { ShopParams } from 'src/app/shared/models/shopParams';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss'],
})
export class ShopComponent implements OnInit {
  @ViewChild('search') searchTerm?: ElementRef;
  products: Product[] = [];
  brands: brand[] = [];
  types: type[] = [];
  shopParams= new ShopParams();
  sortOptions = [
    {name: 'Alphabetical' , value: 'name'},
    {name: 'price: Low To High' , value: 'priceAsc'},
    {name: 'price: High To Low' , value: 'priceDesc'}
  ]
  totalCount = 0;

  constructor(private _ShopService: ShopService) {}

  ngOnInit(): void {
    this.getProducts();
    this.getBrands();
    this.gettypes();
  }
  getProducts() {
    return this._ShopService.getProtducts(this.shopParams).subscribe({
      next: Response =>
       { this.products = Response.data;
        this.shopParams.pageNumber = Response.pageIndex;
        this.shopParams.pageSize = Response.pageSize;
        this.totalCount = Response.count;
      },
      error: (error) => console.log(error),
    });
  }

  getBrands() {
    return this._ShopService.getBrands().subscribe({
      next: (Response) => (this.brands = [{ id: 0, name: 'All' }, ...Response]),
      error: (error) => console.log(error),
    });
  }

  gettypes() {
    return this._ShopService.getTypes().subscribe({
      next: (Response) => (this.types = [{ id: 0, name: 'All' }, ...Response]),
      error: (error) => console.log(error),
    });
  }

  onBrandSelected(brandId: number) {
    this.shopParams.brandId = brandId;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  onTypeSelected(typeId: number) {
    this.shopParams.typeId = typeId;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  onSortSelected(event: any){
       this.shopParams.sort = event.target.value;
       this.getProducts();
  }

  onPageChanged(event: any){
        if(this.shopParams.pageNumber !== event){
           this.shopParams.pageNumber = event;
           this.getProducts();
        }
  }

  onSearch(){
    this.shopParams.search = this.searchTerm?.nativeElement.value;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  onReset(){
    if(this.searchTerm) this.searchTerm.nativeElement.value = '';
    this.shopParams = new ShopParams();
    this.getProducts();
  }
}
