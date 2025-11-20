import type { FullAuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface CityDto extends FullAuditedEntityDto<string> {
  nameAr?: string;
  nameEn?: string;
  countryId?: string;
  countryName?: string;
}

export interface CreateCityDto {
  nameAr: string;
  nameEn: string;
  countryId: string;
}

export interface GetCityListDto extends PagedAndSortedResultRequestDto {
  filter?: string;
  countryId?: string;
}

export interface UpdateCityDto {
  nameAr: string;
  nameEn: string;
  countryId: string;
}
