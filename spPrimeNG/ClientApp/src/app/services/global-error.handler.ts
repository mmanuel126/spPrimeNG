
import { ErrorHandler, Injectable, Injector } from '@angular/core';
import { ErrorLogService } from '../services/error-log.service'
import { NGXLogger } from 'ngx-logger';// **** for logger


@Injectable()
export class GlobalErrorHandler extends ErrorHandler {

    _globalFunctionService: any;
    constructor(private logger: NGXLogger, private errorLogService: ErrorLogService, private inj: Injector) {
        super();
        this._globalFunctionService = inj.get(ErrorLogService);
    }

    handleError(error) {
        // let errorType = this._globalFunctionService.logError(error);
        // this.logger.error(error.message, error.stack);
        // const router = this.inj.get(Router);
        // router.navigate(['/errors'], { queryParams: { errType: errorType } });
    }
}



