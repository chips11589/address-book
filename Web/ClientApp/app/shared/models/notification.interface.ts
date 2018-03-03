export interface AppNotification {
    message: string,
    targetObjectId: string,
    targetObjectName: string,
    notificationType: NotificationTypes
}

export enum NotificationTypes {
    TagAdded = 0,
    TagUpdated = 1,
    TagRemoved = 2
}