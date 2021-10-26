export interface TagChangedNotification extends AppNotification {
    tagId: string,
    tagName: string,
    tagChangedType: TagChangedType
}

export interface AppNotification {
    message: string
}

export enum TagChangedType {
    Added = 0,
    Updated = 1,
    Removed = 2
}