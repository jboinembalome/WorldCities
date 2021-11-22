const path = require('path');
const colors = require('tailwindcss/colors');
const generatePalette = require(path.resolve(__dirname, ('src/app/shared/tailwind/utils/generate-palette')));

/**
 * Custom palettes
 *
 * Uses the generatePalette helper method to generate
 * Tailwind-like color palettes automatically
 */
const customPalettes = {
  orangeSofa: generatePalette({
    300: '#F3CBAC',
    400: '#F3BA8F',
    500: '#EEB68B',
    600: '#EE9F62',
    700: '#E89759',
    800: '#E8761F',
  }),
  nightBlue: generatePalette({
    300: '#8B9AA7',
    400: '#878F97',
    500: '#5D7284',
    600: '#57626E',
    700: '#183650',
    800: '#0F1F30',
  }),
  coolGray: generatePalette({
    200: '#F0F0F0',
    300: '#BBBBBB',
    400: '#A2A2A2',
    500: '#A0A0A0',
    600: '#7D7D7D',
    700: '#787878',
    800: '#464646',
  })
};

/**
* Themes
*/
const themes = {
  // Default theme is required for theming system to work correctly
  'default': {
    primary: {
      ...customPalettes.orangeSofa,
      DEFAULT: customPalettes.orangeSofa[700]
    },
    accent: {
      ...customPalettes.nightBlue,
      DEFAULT: customPalettes.nightBlue[700]
    },
    warn: {
      ...colors.red,
      DEFAULT: colors.red[600]
    },
    'on-warn': {
      500: colors.red['50']
    }
  },
};

module.exports = {
  experimental: {},
  future: {},
  darkMode: 'class',
  important: true,
  mode: 'jit',
  purge: {
    enabled: true,
    content: ['./src/**/*.{html,scss,ts}'],
    options: {
      safelist: {
        standard: ['dark'],
        deep: [/^theme/, /^mat/]
      }
    }
  },
  theme: {
    cursor: {
      auto: 'auto',
      default: 'default',
      pointer: 'pointer',
      wait: 'wait',
      text: 'text',
      move: 'move',
      'not-allowed': 'not-allowed',
      crosshair: 'crosshair',
      'zoom-in': 'zoom-in',
      grab: 'grab'
    },
    extend: {

    },
    colors: {
      transparent: 'transparent',
      current: 'currentColor',
      black: colors.black,
      white: colors.white,
      gray: colors.blueGray,
      green: colors.green,
      indigo: colors.indigo,
      red: colors.red,
      yellow: colors.yellow,
      blue: colors.blue,
      orangeSofa: customPalettes.orangeSofa,
      nightBlue : customPalettes.nightBlue,
      coolGray : customPalettes.coolGray
    }
  },
  variants: {
    extend: {
      backgroundColor: ['disabled'],
      textColor: ['disabled'],
      opacity: ['disabled'],
    }
  },
  plugins: [
    // Tailwind plugins
    require(path.resolve(__dirname, ('src/app/shared/tailwind/plugins/utilities'))),
    require(path.resolve(__dirname, ('src/app/shared/tailwind/plugins/theming')))({themes}),
  ],
};