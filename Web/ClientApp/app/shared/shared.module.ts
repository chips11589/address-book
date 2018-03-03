import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ConfigService } from './utils/config.service';
import { DropDownModalComponent } from './drop-down-modal/drop-down-modal.component';
import { NotificationBarComponent } from './notification-bar/notification-bar.component';
import { TypeaheadDirective } from './typeahead/typeahead.directive';

@NgModule({
    imports: [
        CommonModule, FormsModule
    ],
    declarations: [DropDownModalComponent, NotificationBarComponent, TypeaheadDirective],
    providers: [ConfigService],
    exports: [
        DropDownModalComponent, NotificationBarComponent, TypeaheadDirective
    ]
})
export class SharedModule { }
