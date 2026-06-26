// Service Worker for Cashflow 202 Tracker PWA
// In development mode, this service worker is bypassed.
self.addEventListener('install', () => self.skipWaiting());
self.addEventListener('activate', event => event.waitUntil(clients.claim()));
