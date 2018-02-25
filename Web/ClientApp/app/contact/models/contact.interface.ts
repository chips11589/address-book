export interface Contact {
    id: string,
    firstName: string,
    surname: string,
    companyName: string,
    title: string,
    tags: Tag[]
}

export interface Tag {
    id: string,
    name: string
}