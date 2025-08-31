import { MatCellDef, MatColumnDef, MatHeaderCellDef, MatHeaderRowDef, MatNoDataRow, MatRowDef, MatTableModule } from '@angular/material/table';
import { Component } from '@angular/core';
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
    MatMenuTrigger
],
  templateUrl: './launches.html',
  styleUrl: './launches.scss'
})
export class Launches extends BaseMatTableComponent<LaunchRow> {
  protected displayedColumns: string[] = ['icon', 'name', 'rocketName', 'launchpadName', 'launchDateUtc', 'payloads', 'links', 'status', 'actions'];

  protected SortDirectionEnum = SortDirectionEnum;

  protected showUpcomingOnly: boolean = false;
  protected sortOrder: SortDirectionEnum = SortDirectionEnum.Descending;
  protected includePayloads: boolean = true;

  private data: LaunchResponse[] = [];

  constructor(
    private spaceXService: SpaceXService,
    private dialog: MatDialog,
    spinnerService: SpinnerService
  ) {
    super(spinnerService);
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
          resolve({
            data: this.resolveRowsFromData(result.value?.items),
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
  }

  protected viewDetails(index: number) {
    const selectedLaunch = this.data[index];
    this.dialog.open(LaunchDetailsDialog, {
      data: selectedLaunch
    });
  }

  //#endregion
}