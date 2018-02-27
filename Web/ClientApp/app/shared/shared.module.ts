import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ConfigService } from './utils/config.service';
import { DropDownModalComponent } from './drop-down-modal/drop-down-modal.component';

@NgModule({
    imports: [
        CommonModule, FormsModule
    ],
    declarations: [DropDownModalComponent],
    providers: [ConfigService],
    exports: [
        DropDownModalComponent
    ]
})
export class SharedModule { }