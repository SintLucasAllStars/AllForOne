module.exports = {
    title: "AllForOne documentation",
    description: "This is the documentation for the AllForOne project for the seconds years",
    plugins: [['vuepress-plugin-code-copy', true]],
    themeConfig: {
        smoothScroll: true,
        nav: [
            { text: 'Home', link: '/' },
            { text: 'Docs', link: '/Scripts/' },
            { text: 'How to use', link: '/HowToUse/' },
            { text: 'Contact', link: '/Contact' }
        ],
        sidebar: {
            '/Scripts/':
                [
                    '',
                    'IExtraFunctions',
                    'Singleton'
                ],
            '/HowToUse/':
                [
                    '',
                    'Importing',
                    'Usage',
                    'NewConcepts',
                    'Docker'
                ]
        }
    },
    markdown: {
        lineNumbers: true
    }
}