export interface AuthStoreState {
  email: string;
  emailVerified: boolean;
  username: string;
  groups: string[];
  isAdmin: boolean;
  _isAuthenticated: boolean;
}

export const AuthStoreInitialState: AuthStoreState = {
  email: "",
  emailVerified: false,
  username: "",
  groups: [],
  isAdmin: false,
  _isAuthenticated: false,
};

export interface IdToken {
  email: string;
  email_verified: boolean;
  exp: number;
  "cognito:username": string;
  "cognito:groups": string[];
}

export interface PKCE {
  codeChallenge: string;
  codeVerifier: string;
}
