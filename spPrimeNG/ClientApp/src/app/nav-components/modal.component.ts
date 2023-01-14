import { Component, ElementRef, Input } from '@angular/core';

import { ModalsService } from '../services/modal.services';

@Component({
    selector: 'jw-modal',
    template:
        `<div class="jw-modal">
            <div class="jw-modal-body">
                <ng-content></ng-content>
            </div>
        </div>
        <div class="jw-modal-background"></div>`
})
export class ModalComponent {
    @Input() id: string;
    private element: any;

    constructor(private modalService: ModalsService, private el: ElementRef) {
        this.element = el.nativeElement;
    }

    // remove self from modal service when component is destroyed
    ngOnDestroy(): void {
        this.modalService.remove(this.id);
        this.element.remove();
    }

    // open modal
    open(): void {
        this.element.style.display = 'block';
        document.body.classList.add('jw-modal-open');
    }

    // close modal
    close(): void {
        this.element.style.display = 'none';
        document.body.classList.remove('jw-modal-open');
    }
}
