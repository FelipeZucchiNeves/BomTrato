export class Office{
    id: string;
    street: string;
    number: string;
    state: string;
    cep: number;
    city: string;
    district: string;
}


export class GetCep {
    cep: string;
    logradouro: string;
    complemento: string;
    bairro: string;
    localidade: string;
    uf: string;
}