import { defineConfig } from 'vitest/config';

export default defineConfig({
  test: {
    environment: 'jsdom', // Fundamental para Vanilla JS
    coverage: {
      provider: 'v8',
      reporter: ['text', 'html'], // 'text' para la terminal, 'html' para el navegador
      include: ['**/*.js'], // Archivos a medir
      exclude: ['node_modules/**', 'vitest.config.js'], // Archivos a ignorar
      globals: true
    },
  },
});