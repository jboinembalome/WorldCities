export interface CreateCountryCommand {
    id: number;
    name: string;
    iso2: string;
    iso3: string;
    //cityIds: number[];
}