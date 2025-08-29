import { HttpParams } from "@angular/common/http";

export type params = { [param: string]: string | number | boolean | ReadonlyArray<string | number | boolean>; };
export type httpParams = | HttpParams | params;
export type patchData = { op: string, path: string, value: any };
export type httpHeaders = string | { [name: string]: string | number | (string | number)[]; } | Headers;