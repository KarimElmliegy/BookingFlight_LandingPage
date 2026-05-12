import { HttpInterceptorFn } from '@angular/common/http';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const token = localStorage.getItem('token');

  console.log('AuthInterceptor - URL:', req.url);
  console.log('AuthInterceptor - Token exists:', !!token);

  if (token) {
    console.log('AuthInterceptor - Adding Bearer token to request');
    const cloned = req.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`,
      },
    });
    return next(cloned);
  }

  console.warn('AuthInterceptor - No token found, request will be unauthenticated');
  return next(req);
};
