export const client = async (url, options = {}) => {
  const baseURL = import.meta.env.VITE_API_URL;
  const accessToken = localStorage.getItem('access_token');

  const defaultOptions = {
    headers: {
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${accessToken}`,
      ...options?.headers,
    },
  };

  const response = await fetch(`${baseURL}${url}`, {
    ...defaultOptions,
    ...options,
  });

  const hasContent = response.headers.get('Content-Length') > 0 || 
                     response.headers.get('Content-Type')?.includes('application/json');

  if (!response.ok) {
    if (hasContent) {
      const errorResponse = await response.json();
      return {
        error: true,
        status: response.status,
        message: errorResponse.message,
      };
    }
    return {
      error: true,
      status: response.status,
      message: 'An error occurred, but no message was provided by the server.',
    };
  }

  return (hasContent) ? response.json() : {};
};