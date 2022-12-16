/**
 * Response to valid login request.
 */
interface LogInResponse {
    userId: number;
    userTitle: string;
    jwt: string;
    userRole: number;
};
interface SuccessfulLoginResponseDto {
    AccessToken: string;
}
/**
 * Entity for creating and updating.
 */
class RegisterDto {
    slapyvardis: string = "";
    slaptazodis: string = "";
    el_pastas: string = "";
}
interface UserDto {
    Id: string;
    UserName: string;
    Email: string;
};


//
export type {
    LogInResponse
}
export type {
    SuccessfulLoginResponseDto
}
export {
    RegisterDto
}
export type {
    UserDto
}
