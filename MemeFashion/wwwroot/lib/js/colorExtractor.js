function rgbToHex(r, g, b) {
    return '#' + [r, g, b].map(x => {
        const hex = x.toString(16);
        return hex.length === 1 ? '0' + hex : hex;
    }).join('');
}

function extractColors(imagePath) {
    console.log('extractColors called with:', imagePath);
    return new Promise((resolve, reject) => {
        const img = new Image();
        img.crossOrigin = "Anonymous";
        
        img.onload = function() {
            console.log('Image loaded successfully');
            try {
                const canvas = document.createElement('canvas');
                const ctx = canvas.getContext('2d');
                canvas.width = img.width;
                canvas.height = img.height;
                ctx.drawImage(img, 0, 0);
                const imageData = ctx.getImageData(0, 0, canvas.width, canvas.height).data;
                const colors = {};
                
                // Sample colors from the image
                for(let i = 0; i < imageData.length; i += 40) {
                    const r = imageData[i];
                    const g = imageData[i + 1];
                    const b = imageData[i + 2];
                    const hex = rgbToHex(r, g, b);
                    colors[hex] = (colors[hex] || 0) + 1;
                }
                
                const sortedColors = Object.entries(colors)
                    .sort(([,a], [,b]) => b - a)
                    .slice(0, 5)
                    .map(([color]) => color);
                
                console.log('Extracted colors:', sortedColors);
                resolve(sortedColors);
            } catch (error) {
                console.error('Error processing image:', error);
                reject(error);
            }
        };
        
        img.onerror = function(error) {
            console.error('Error loading image:', error);
            reject(error);
        };
        
        img.src = imagePath;
    });
}
