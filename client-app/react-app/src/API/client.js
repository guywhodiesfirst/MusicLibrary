export const client = async (url, options = {}) => {
  const baseURL = import.meta.env.VITE_API_URL;
  const accessToken = localStorage.getItem('access_token');

  const defaultOptions = {
    headers: {
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${accessToken}`,
      ...options.headers,
    },
  };

  try {
    const response = await fetch(`${baseURL}${url}`, {
      ...defaultOptions,
      ...options,
    });

    if (!response.ok) {
      if (response.status === 404) {
        return { error: true, status: 404, message: 'Not found' };
      }

      if (response.headers.get('Content-Type')?.includes('application/json')) {
        const errorResponse = await response.json();
        return { error: true, status: response.status, message: errorResponse.message || 'Server error' };
      }

      return { error: true, status: response.status, message: 'An error occurred' };
    }

    return response.ok && response.headers.get('Content-Type')?.includes('application/json')
      ? await response.json()
      : {};
  } catch (error) {
    console.error('Network or unexpected error:', error);
    return { error: true, status: 500, message: 'Network error or unexpected issue' };
  }
};
