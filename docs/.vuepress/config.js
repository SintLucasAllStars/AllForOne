module.exports = {
    title: "AllForOne documentation",
    description: "This is the documentation for the AllForOne project for the seconds years",
    theme: 'default-prefers-color-scheme',
    themeConfig: {
        defaultTheme: 'dark'
    },
    plugins: [['vuepress-plugin-code-copy', true]],
    themeConfig: {
        nav: [
            { text: 'Home', link: '/' },
            { text: 'Docs', link: '/Scripts/' },
            { text: 'How to use', link: '/HowToUse/' },
        ],
        sidebar: {
            '/Scripts/':
                [
                    '',
                    'IExtraFunctions',
                    'Singleton'
                ],
            'HowToUse': [
                ''
            ]
        }
    }
}