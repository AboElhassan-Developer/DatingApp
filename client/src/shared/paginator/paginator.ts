import { Component, computed, input, model, output } from '@angular/core';

@Component({
  selector: 'app-paginator',
  imports: [],
  templateUrl: './paginator.html',
  styleUrl: './paginator.css',
})
export class Paginator {
  pageNumber = model(1);
  pageSize = model(5);
  totalCount = input(0);
  totalPages = input(0);
  pageSizeOptions = input([5, 10, 15, 20]);


  pageChange = output<{ pageNumber: number, pageSize: number }>();

  lastItemIndex = computed(() => {
    return Math.min(this.pageNumber() * this.pageSize(), this.totalCount())
  })

  onPageChange(newPage?: number, pageSize?: EventTarget | null) {
  const currentPage = newPage ?? this.pageNumber();
  const currentSize = pageSize 
    ? Number((pageSize as HTMLSelectElement).value) 
    : this.pageSize();
  this.pageNumber.set(currentPage);
  this.pageSize.set(currentSize);
  this.pageChange.emit({
    pageNumber: currentPage,
    pageSize: currentSize
  });
}
}
