export interface ProblemDetailsData {
  problemDetails: ProblemDetails
  contentType: string
  statusCode: number
}

export interface ProblemDetails {
  type: string
  title: string
  status: number
  meta: Meta
}

export interface Meta {
  code: string
  description: string
}

export class ApplicationError {
  code?: string;
  message: string = "";
}

export const ApplicationErrorGenFactory = (message: string, code?: string) => {
  const error = new ApplicationError();
  error.code = code;
  error.message = message;
  return error;
};

export const ApplicationErrorProblemDetailsFactory = (problemDetails: ProblemDetailsData) => {
  const error = new ApplicationError();
  error.code = problemDetails.problemDetails.meta.code;
  error.message = problemDetails.problemDetails.meta.description;
  return error;
};