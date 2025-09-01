import { AfterViewInit, Directive, signal, ViewChild, WritableSignal } from "@angular/core";
import { MatPaginator, PageEvent } from "@angular/material/paginator";
import { MatTableDataSource } from "@angular/material/table";
import { MatIcons } from "../../enums/ui/mat-icons.enum";
import { SpinnerService } from "../../services/core/ui/spinner.service";
import * as commonConst from "../../constants/common.constants";

@Directive()
export abstract class BaseMatTableComponent<TData extends object = any> implements AfterViewInit {
    protected dataSource: MatTableDataSource<TData> = new MatTableDataSource<TData>();

    protected readonly pageSizeOptions: number[] = [commonConst.TABLE_PAGE_SIZE_OPTION_1, commonConst.TABLE_PAGE_SIZE_OPTION_2, commonConst.TABLE_PAGE_SIZE_OPTION_3, commonConst.TABLE_PAGE_SIZE_OPTION_4];

    protected pageIndex: WritableSignal<number> = signal(0);
    protected pageSize: WritableSignal<number> = signal(this.pageSizeOptions[1]);
    protected totalRows: WritableSignal<number> = signal(0);

    protected isFetching: boolean = false;

    protected menuOpenState: boolean[] = []; 
    
    protected abstract displayedColumns: string[];

    protected readonly EMPTY_TABLE_STRING: string = commonConst.EMPTY_TABLE_STRING;
    protected readonly EMPTY_TABLE_FIELD_STRING: string = commonConst.EMPTY_TABLE_FIELD_STRING;

    protected MatIcons = MatIcons;

    @ViewChild(MatPaginator) paginator!: MatPaginator;

    public ngAfterViewInit(): void {
        this.paginator.pageSize = this.pageSize();
        this.fetchData();
    }

    protected abstract dataFetchingMethod(): Promise<{ data: TData[], totalRows?: number }>;

    protected async fetchData(): Promise<TData[]> {
        return new Promise<TData[]>((resolve, reject) => {
            if(this.isFetching) { resolve([]); return; }

            this.isFetching = true;
            this.dataSource.data = [];

            this.dataFetchingMethod()
                .then(response => {
                    this.dataSource.data = response.data;
                    if (response.totalRows !== undefined) {
                        this.totalRows.set(response.totalRows);
                        this.paginator.length = response.totalRows; 
                    } else {
                        this.dataSource.paginator = this.paginator;
                    }
                    resolve(response.data);
                })
                .catch(error => {
                    reject(error);
                }).finally(() =>{
                    this.isFetching = false;
                });
        });
    }

    public changePage(event: PageEvent): void {
        const newPageSize = event.pageSize;
        const newPageIndex = event.pageIndex;

        if (newPageSize !== this.pageSize()) {
            this.pageSize.set(newPageSize);
            this.pageIndex.set(0);
            this.paginator.firstPage();
        } else {
            this.pageIndex.set(newPageIndex);
        }

        this.fetchData();
    }
}