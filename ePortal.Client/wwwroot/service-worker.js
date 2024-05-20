// In development, always fetch from the network and do not enable offline support.
// This is because caching would make development more difficult (changes would not
// be reflected on the first load after each change).

// Return to this if error
//self.addEventListener('fetch', () => { });

self.addEventListener('fetch', event => {
    // You can add logic here to handle fetch events, such as caching strategies.
    // For example, using a cache-first strategy:
    event.respondWith(
        caches.match(event.request).then(response => {
            return response || fetch(event.request);
        })
    );
});
