define(['./template.js', '../lib/showdown/showdown.js', './clientStorage.js'], function (template, showdown, clientStorage) {

    var blogLatestPostsUrl = '/Home/LatestBlogPosts/';
    var blogPostUrl = '/Home/Post/?link=';
    var blogMorePostsUrl = '/Home/MoreBlogPosts/?oldestBlogPostId=';

    function fetchPromise(url, link, text) {

        link = link || '';

        return new Promise(function (resolve, reject) {
            fetch(url + link)
                .then(function (data) {

                    var resolveSuccess = function () {
                        resolve('A conexão está boa, mostrando os últimos resultados.');
                        document.getElementById("connection-status").removeAttribute("class", "show");
                        document.getElementById("connection-status").setAttribute("class", "hide");                        
                    };

                    if (text) {
                        data.text().then(function (text) {
                            clientStorage.addPostText(link, text).then(resolveSuccess);
                        });
                    }
                    else {
                        data.json().then(function (jsonData) {
                            clientStorage.addPosts(jsonData).then(resolveSuccess);
                        });
                    }

                }).catch(function (e) {
                    resolve('Sem conexão, mostrando resultados offline.');
                    document.getElementById("connection-status").removeAttribute("class", "hide");
                    document.getElementById("connection-status").setAttribute("class", "show");
                    document.getElementById("connection-status").setAttribute("class", "alert alert-danger show");
                });

            setTimeout(function () {
                resolve('A conexão está interrompida, mostrando resultados offline.');
                //document.getElementById("connection-status").removeAttribute("class", "hide");
                //document.getElementById("connection-status").setAttribute("class", "show");
                //document.getElementById("connection-status").setAttribute("class", "alert alert-warning show");
            }, 800);
        });
    }

    function loadData(url) {
        fetchPromise(url)
            .then(function (status) {
                $('#connection-status').html(status);

                clientStorage.getPosts()
                    .then(function (posts) {
                        template.appendBlogList(posts);
                    })
            });
    }

    function loadLatestBlogPosts() {
        loadData(blogLatestPostsUrl);
    }

    function loadBlogPost(link) {

        fetchPromise(blogPostUrl, link, true)
            .then(function (status) {
                $('#connection-status').html(status);

                clientStorage.getPostText(link)
                    .then(function (data) {
                        if (!data) {

                            var contentNotFound = $('#blog-content-not-found')
                                .html().replace(/{{Link}}/g, link)
                                ;

                            template.showBlogItem(contentNotFound, link);
                        } else {
                            var converter = new showdown.Converter();
                            html = converter.makeHtml(data);
                            template.showBlogItem(html, link);
                        }
                        window.location = '#' + link;
                    })
            });
    }

    function loadMoreBlogPosts() {
        loadData(blogMorePostsUrl + clientStorage.getOldestBlogPostId());
    }

    return {
        loadLatestBlogPosts: loadLatestBlogPosts,
        loadBlogPost: loadBlogPost,
        loadMoreBlogPosts: loadMoreBlogPosts
    }
});