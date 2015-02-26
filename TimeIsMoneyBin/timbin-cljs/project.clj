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
                 [org.clojure/clojurescript "0.0-2511"]]
  :plugins [[lein-cljsbuild "1.0.5"]]
  :cljsbuild {:builds []})
