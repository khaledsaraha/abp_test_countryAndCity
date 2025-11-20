import type { CityDto, CreateCityDto, GetCityListDto, UpdateCityDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class CityService {
  apiName = 'Default';
  

  create = (input: CreateCityDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CityDto>({
      method: 'POST',
      url: '/api/app/city',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/city/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CityDto>({
      method: 'GET',
      url: `/api/app/city/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: GetCityListDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<CityDto>>({
      method: 'GET',
      url: '/api/app/city',
      params: { filter: input.filter, countryId: input.countryId, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: UpdateCityDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CityDto>({
      method: 'PUT',
      url: `/api/app/city/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
