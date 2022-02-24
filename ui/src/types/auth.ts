export interface AuthStoreState {
  email: string;
  emailVerified: boolean;
  username: string;
  isAdmin: boolean;
  _isAuthenticated: boolean;
}

export const AuthStoreInitialState: AuthStoreState = {
  email: "",
  emailVerified: false,
  username: "",
  isAdmin: false,
  _isAuthenticated: false,
};

export interface IdToken {
  email: string;
  email_verified: boolean;
  exp: number;
}

export interface PKCE {
  codeChallenge: string;
  codeVerifier: string;
}
