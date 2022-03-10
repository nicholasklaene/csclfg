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
