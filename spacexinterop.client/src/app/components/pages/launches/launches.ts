import { MatCellDef, MatColumnDef, MatHeaderCellDef, MatHeaderRowDef, MatNoDataRow, MatRowDef, MatTableModule } from '@angular/material/table';
import { Component, OnInit } from '@angular/core';
import { BaseMatTableComponent } from '../../../shared/classes/ui/base-mat-table-component';
import { LaunchRow } from '../../../shared/classes/ui/view/launch-row';
import { SpinnerService } from '../../../shared/services/core/ui/spinner.service';
import { SpaceXService } from '../../../shared/services/client/spacex.service';
import { SpaceXLaunchesRequest } from '../../../shared/classes/models/requests/SpaceXLaunchesRequest.model';
import { SortDirectionEnum } from '../../../shared/enums/api/SortDirectionEnum';
import { LaunchResponse } from '../../../shared/classes/models/responses/LaunchResponse.model';
import { MatPaginator } from '@angular/material/paginator';
import { MatFormField, MatLabel } from '@angular/material/form-field';
import { MatInput } from '@angular/material/input';
import { MatOption, MatSelect } from '@angular/material/select';
import { FormsModule } from '@angular/forms';
import { NgClass } from '@angular/common';
import { MatIcon } from '@angular/material/icon';
import { MatMenu, MatMenuTrigger } from '@angular/material/menu';
import { MatDialog } from '@angular/material/dialog';
import { LaunchDetailsDialog } from './launch-details-dialog/launch-details-dialog';
import { Subject, takeUntil } from 'rxjs';
import { WindowDimensionsService } from '../../../shared/services/core/ui/window-dimension.service';
import { WindowDimensions } from '../../../shared/interfaces/services/window-dimensions.interface';
import { GenericButton } from "../../core/generic-button/generic-button";

@Component({
  selector: 'app-launches',
  imports: [
    MatColumnDef,
    MatCellDef,
    MatRowDef,
    MatHeaderCellDef,
    MatHeaderRowDef,
    MatNoDataRow,
    MatPaginator,
    MatTableModule,
    MatLabel,
    MatFormField,
    MatInput,
    MatSelect,
    MatOption,
    FormsModule,
    NgClass,
    MatIcon,
    MatMenu,
    MatMenuTrigger,
    GenericButton
],
  templateUrl: './launches.html',
  styleUrl: './launches.scss'
})
export class Launches extends BaseMatTableComponent<LaunchRow> implements OnInit {
  protected displayedColumns: string[] = ['icon', 'name', 'rocketName', 'launchpadName', 'launchDateUtc', 'payloads', 'links', 'status', 'actions'];

  protected SortDirectionEnum = SortDirectionEnum;

  protected showUpcomingOnly: boolean = false;
  protected sortOrder: SortDirectionEnum = SortDirectionEnum.Descending;
  protected includePayloads: boolean = true;

  private data: LaunchResponse[] = [];
  protected filteredData: LaunchRow[] = [];

  protected windowDimensions: WindowDimensions = {} as WindowDimensions;

  private unsubscribe$ = new Subject<void>();

  constructor(
    private spaceXService: SpaceXService,
    private windowDimensionsService: WindowDimensionsService,
    private dialog: MatDialog,
    spinnerService: SpinnerService
  ) {
    super(spinnerService);
  }

  public ngOnInit(): void {
    this.createSubscriptions();
  }

  private createSubscriptions(): void {
      this.windowDimensionsService.getWindowDimensions$().pipe(takeUntil(this.unsubscribe$)).subscribe(dimensions => {
        this.windowDimensions = dimensions;
      });
  }

  protected override async dataFetchingMethod(): Promise<{ data: LaunchRow[]; totalRows?: number; }> {
    return new Promise(async (resolve, reject) => {
      const request: SpaceXLaunchesRequest = {
        upcoming: this.showUpcomingOnly,
        sortDirection: this.sortOrder,
        pageIndex: this.pageIndex(),
        pageSize: this.pageSize(),
        includePayloads: this.includePayloads
      };

      await this.spaceXService.getLaunches(request).then((result) => {
        if (result.isSuccess && result.value?.items) {
          this.data = result.value.items;
          const rows = this.resolveRowsFromData(result.value?.items);
          this.filteredData = rows;
          resolve({
            data: rows,
            totalRows: result.value.totalItems
          });
        } else {
          reject({ data: [] });
        }
      }).catch((error) => {
        reject(error);
      });
    });
  }

  private resolveRowsFromData(data: LaunchResponse[]): LaunchRow[] {
    return data.map(item => {
      const formattedDate = new Date(item.launchDateUtc).toLocaleDateString("en-US", {
        month: "short",
        day: "numeric",
        year: "numeric",
        hour: "2-digit",
        minute: "2-digit"
      });

      return new LaunchRow(
        item.missionPatchImage,
        item.name,
        item.rocketName,
        item.launchpadName,
        formattedDate,
        item.upcoming,
        item.success,
        item.webcastUrl,
        item.wikipediaUrl,
        item.articleUrl,
        item.payloads.length
      );
    });
  }

  //#region UI Methods

  protected applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value.trim().toLowerCase();
    this.dataSource.filterPredicate = (data: LaunchRow, filter: string) =>
    data.name.toLowerCase().includes(filter);
    this.dataSource.filter = filterValue;

    this.filteredData = this.dataSource.filteredData;
  }

  protected viewDetails(index: number) {
    const selectedLaunch = this.data[index];
    this.dialog.open(LaunchDetailsDialog, {
      data: selectedLaunch
    });
  }

  //#endregion
}