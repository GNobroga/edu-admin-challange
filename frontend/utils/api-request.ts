export interface ResponseError {
  title: string;
  statusCode: number;
  errors: string[];
}

export interface FetchOptions {
  method: string;
  headers?: HeadersInit;
  body?: BodyInit | null;
  mode?: RequestMode;
  credentials?: RequestCredentials;
  cache?: RequestCache;
  redirect?: RequestRedirect;
  referrerPolicy?: ReferrerPolicy;
}


export async function apiRequest<T>(url: string,
    data: any, resolve: (data: T) => void = () => {},
    reject: (error: ResponseError) => void = () => {},
    options?: FetchOptions
  ): Promise<void> {
  try {
    const response = await fetch(url, {
      ...options,
      headers: {
        ...options?.headers,
        'Content-Type': 'application/json',
      },
      body: data ? JSON.stringify(data) : null,
    });

    const json = await response.json();

    if (!response.ok) {
      if (reject) reject(json);
      return;
    }
    if (resolve) resolve(json.data as T);
  } catch(error) {
    console.log(error);
  }
}
