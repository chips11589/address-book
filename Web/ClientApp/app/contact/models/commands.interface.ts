import { Tag } from "./entities.interface";

export interface UpdateContactTagsCommand {
    contactId: string,
    tags: Tag[],
}

export interface CreateTagCommand {
    tag: Tag
}

export interface UpdateTagCommand {
    tag: Tag
}

export interface DeleteTagCommand {
    id: string
}