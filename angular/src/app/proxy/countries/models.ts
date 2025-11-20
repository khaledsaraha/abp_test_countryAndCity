import type { FullAuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface CountryDto extends FullAuditedEntityDto<string> {
  nameAr?: string;
  nameEn?: string;
}

export interface CreateCountryDto {
  nameAr: string;
  nameEn: string;
}

export interface GetCountryListDto extends PagedAndSortedResultRequestDto {
  filter?: string;
}

export interface UpdateCountryDto {
  nameAr: string;
  nameEn: string;
}
