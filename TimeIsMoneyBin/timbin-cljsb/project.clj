(defproject timbin-cljs "0.1.0-SNAPSHOT"
  :description "FIXME: write description"
  :url "http://example.com/FIXME"
  :license {:name "Eclipse Public License"
            :url "http://www.eclipse.org/legal/epl-v10.html"}
  :dependencies [[org.clojure/clojure "1.6.0"]
                 ;; Even though the latest version of ClojureScript is
                 ;; 2913, this version causes warnings about multiple
                 ;; declarations of the google.closure namespace. Even
                 ;; though these are warnings, they seem to cause some
                 ;; instability with the interpreter. version 2511
                 ;; seems to be the last "clean" interpreter.
                 [org.clojure/clojurescript "0.0-2511"]
                 [compojure "1.3.2"]
                 [ring/ring-jetty-adapter "1.3.2"]]
  ;; We need to add src/cljs too, because cljsbuild does not add its
  ;; source-paths to the project source-paths
  :source-paths ["src/clj" "src/cljs" "test/cljs"]

  :plugins [[lein-cljsbuild "1.0.5"]
            [lein-ring "0.9.2"]]

  :hooks [leiningen.cljsbuild]

  :ring {:handler timbin-cljsb.core/handler}

  :cljsbuild
  {:builds {;; This build is only used for including any cljs source
            ;; in the packaged jar when you issue lein jar command and
            ;; any other command that depends on it
            :timbin-cljs
            {:source-paths ["src/cljs"]
             ;; The :jar true option is not needed to include the CLJS
             ;; sources in the packaged jar. This is because we added
             ;; the CLJS source codebase to the Leiningen
             ;; :source-paths
             ;:jar true
             ;; Compilation Options
             :compiler
             {:output-to "resources/public/js/timbin_cljsb.js"
              :optimizations :whitespace
              :pretty-print true}}}})
