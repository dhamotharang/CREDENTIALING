//$(document).ready(function () {
//    $('#calendar').eCalendar({

// weekDays: ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'],
//        months: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'],
//        textArrows: {previous: '<', next: '>'},
//        eventTitle: 'Events',
//        url: '',
//        events: [
//            { title: ' Smith : FollowUp', description: '10AM - 11AM', datetime: new Date(2016, 7, 25, 10) },
//            { title: ' Alena : FollowUp', description: '12PM - 01PM', datetime: new Date(2016, 7, 27, 12) },
//            { title: ' John  : FollowUp', description: '01:30PM - 02PM', datetime: new Date(2016, 7, 28, 01, 30) },
//            { title: ' Jarvis  : FollowUp', description: '01:30PM - 02PM', datetime: new Date(2016, 7, 30, 01, 30) }
//        ]});
//});











!function (a, b) { "object" == typeof exports && "object" == typeof module ? module.exports = b() : "function" == typeof define && define.amd ? define(b) : "object" == typeof exports ? exports.Handlebars = b() : a.Handlebars = b() }(this, function () {
    return function (a) { function b(d) { if (c[d]) return c[d].exports; var e = c[d] = { exports: {}, id: d, loaded: !1 }; return a[d].call(e.exports, e, e.exports, b), e.loaded = !0, e.exports } var c = {}; return b.m = a, b.c = c, b.p = "", b(0) }([function (a, b, c) { "use strict"; function d() { var a = r(); return a.compile = function (b, c) { return k.compile(b, c, a) }, a.precompile = function (b, c) { return k.precompile(b, c, a) }, a.AST = i["default"], a.Compiler = k.Compiler, a.JavaScriptCompiler = m["default"], a.Parser = j.parser, a.parse = j.parse, a } var e = c(8)["default"]; b.__esModule = !0; var f = c(1), g = e(f), h = c(2), i = e(h), j = c(3), k = c(4), l = c(5), m = e(l), n = c(6), o = e(n), p = c(7), q = e(p), r = g["default"].create, s = d(); s.create = d, q["default"](s), s.Visitor = o["default"], s["default"] = s, b["default"] = s, a.exports = b["default"] }, function (a, b, c) { "use strict"; function d() { var a = new h.HandlebarsEnvironment; return n.extend(a, h), a.SafeString = j["default"], a.Exception = l["default"], a.Utils = n, a.escapeExpression = n.escapeExpression, a.VM = p, a.template = function (b) { return p.template(b, a) }, a } var e = c(9)["default"], f = c(8)["default"]; b.__esModule = !0; var g = c(10), h = e(g), i = c(11), j = f(i), k = c(12), l = f(k), m = c(13), n = e(m), o = c(14), p = e(o), q = c(7), r = f(q), s = d(); s.create = d, r["default"](s), s["default"] = s, b["default"] = s, a.exports = b["default"] }, function (a, b, c) { "use strict"; b.__esModule = !0; var d = { helpers: { helperExpression: function (a) { return "SubExpression" === a.type || ("MustacheStatement" === a.type || "BlockStatement" === a.type) && !!(a.params && a.params.length || a.hash) }, scopedId: function (a) { return /^\.|this\b/.test(a.original) }, simpleId: function (a) { return 1 === a.parts.length && !d.helpers.scopedId(a) && !a.depth } } }; b["default"] = d, a.exports = b["default"] }, function (a, b, c) { "use strict"; function d(a, b) { if ("Program" === a.type) return a; h["default"].yy = n, n.locInfo = function (a) { return new n.SourceLocation(b && b.srcName, a) }; var c = new j["default"](b); return c.accept(h["default"].parse(a)) } var e = c(8)["default"], f = c(9)["default"]; b.__esModule = !0, b.parse = d; var g = c(15), h = e(g), i = c(16), j = e(i), k = c(17), l = f(k), m = c(13); b.parser = h["default"]; var n = {}; m.extend(n, l) }, function (a, b, c) { "use strict"; function d() { } function e(a, b, c) { if (null == a || "string" != typeof a && "Program" !== a.type) throw new k["default"]("You must pass a string or Handlebars AST to Handlebars.precompile. You passed " + a); b = b || {}, "data" in b || (b.data = !0), b.compat && (b.useDepths = !0); var d = c.parse(a, b), e = (new c.Compiler).compile(d, b); return (new c.JavaScriptCompiler).compile(e, b) } function f(a, b, c) { function d() { var d = c.parse(a, b), e = (new c.Compiler).compile(d, b), f = (new c.JavaScriptCompiler).compile(e, b, void 0, !0); return c.template(f) } function e(a, b) { return f || (f = d()), f.call(this, a, b) } if (void 0 === b && (b = {}), null == a || "string" != typeof a && "Program" !== a.type) throw new k["default"]("You must pass a string or Handlebars AST to Handlebars.compile. You passed " + a); "data" in b || (b.data = !0), b.compat && (b.useDepths = !0); var f = void 0; return e._setup = function (a) { return f || (f = d()), f._setup(a) }, e._child = function (a, b, c, e) { return f || (f = d()), f._child(a, b, c, e) }, e } function g(a, b) { if (a === b) return !0; if (l.isArray(a) && l.isArray(b) && a.length === b.length) { for (var c = 0; c < a.length; c++) if (!g(a[c], b[c])) return !1; return !0 } } function h(a) { if (!a.path.parts) { var b = a.path; a.path = { type: "PathExpression", data: !1, depth: 0, parts: [b.original + ""], original: b.original + "", loc: b.loc } } } var i = c(8)["default"]; b.__esModule = !0, b.Compiler = d, b.precompile = e, b.compile = f; var j = c(12), k = i(j), l = c(13), m = c(2), n = i(m), o = [].slice; d.prototype = { compiler: d, equals: function (a) { var b = this.opcodes.length; if (a.opcodes.length !== b) return !1; for (var c = 0; b > c; c++) { var d = this.opcodes[c], e = a.opcodes[c]; if (d.opcode !== e.opcode || !g(d.args, e.args)) return !1 } b = this.children.length; for (var c = 0; b > c; c++) if (!this.children[c].equals(a.children[c])) return !1; return !0 }, guid: 0, compile: function (a, b) { this.sourceNode = [], this.opcodes = [], this.children = [], this.options = b, this.stringParams = b.stringParams, this.trackIds = b.trackIds, b.blockParams = b.blockParams || []; var c = b.knownHelpers; if (b.knownHelpers = { helperMissing: !0, blockHelperMissing: !0, each: !0, "if": !0, unless: !0, "with": !0, log: !0, lookup: !0 }, c) for (var d in c) d in c && (b.knownHelpers[d] = c[d]); return this.accept(a) }, compileProgram: function (a) { var b = new this.compiler, c = b.compile(a, this.options), d = this.guid++; return this.usePartial = this.usePartial || c.usePartial, this.children[d] = c, this.useDepths = this.useDepths || c.useDepths, d }, accept: function (a) { if (!this[a.type]) throw new k["default"]("Unknown type: " + a.type, a); this.sourceNode.unshift(a); var b = this[a.type](a); return this.sourceNode.shift(), b }, Program: function (a) { this.options.blockParams.unshift(a.blockParams); for (var b = a.body, c = b.length, d = 0; c > d; d++) this.accept(b[d]); return this.options.blockParams.shift(), this.isSimple = 1 === c, this.blockParams = a.blockParams ? a.blockParams.length : 0, this }, BlockStatement: function (a) { h(a); var b = a.program, c = a.inverse; b = b && this.compileProgram(b), c = c && this.compileProgram(c); var d = this.classifySexpr(a); "helper" === d ? this.helperSexpr(a, b, c) : "simple" === d ? (this.simpleSexpr(a), this.opcode("pushProgram", b), this.opcode("pushProgram", c), this.opcode("emptyHash"), this.opcode("blockValue", a.path.original)) : (this.ambiguousSexpr(a, b, c), this.opcode("pushProgram", b), this.opcode("pushProgram", c), this.opcode("emptyHash"), this.opcode("ambiguousBlockValue")), this.opcode("append") }, DecoratorBlock: function (a) { var b = a.program && this.compileProgram(a.program), c = this.setupFullMustacheParams(a, b, void 0), d = a.path; this.useDecorators = !0, this.opcode("registerDecorator", c.length, d.original) }, PartialStatement: function (a) { this.usePartial = !0; var b = a.program; b && (b = this.compileProgram(a.program)); var c = a.params; if (c.length > 1) throw new k["default"]("Unsupported number of partial arguments: " + c.length, a); c.length || (this.options.explicitPartialContext ? this.opcode("pushLiteral", "undefined") : c.push({ type: "PathExpression", parts: [], depth: 0 })); var d = a.name.original, e = "SubExpression" === a.name.type; e && this.accept(a.name), this.setupFullMustacheParams(a, b, void 0, !0); var f = a.indent || ""; this.options.preventIndent && f && (this.opcode("appendContent", f), f = ""), this.opcode("invokePartial", e, d, f), this.opcode("append") }, PartialBlockStatement: function (a) { this.PartialStatement(a) }, MustacheStatement: function (a) { this.SubExpression(a), a.escaped && !this.options.noEscape ? this.opcode("appendEscaped") : this.opcode("append") }, Decorator: function (a) { this.DecoratorBlock(a) }, ContentStatement: function (a) { a.value && this.opcode("appendContent", a.value) }, CommentStatement: function () { }, SubExpression: function (a) { h(a); var b = this.classifySexpr(a); "simple" === b ? this.simpleSexpr(a) : "helper" === b ? this.helperSexpr(a) : this.ambiguousSexpr(a) }, ambiguousSexpr: function (a, b, c) { var d = a.path, e = d.parts[0], f = null != b || null != c; this.opcode("getContext", d.depth), this.opcode("pushProgram", b), this.opcode("pushProgram", c), d.strict = !0, this.accept(d), this.opcode("invokeAmbiguous", e, f) }, simpleSexpr: function (a) { var b = a.path; b.strict = !0, this.accept(b), this.opcode("resolvePossibleLambda") }, helperSexpr: function (a, b, c) { var d = this.setupFullMustacheParams(a, b, c), e = a.path, f = e.parts[0]; if (this.options.knownHelpers[f]) this.opcode("invokeKnownHelper", d.length, f); else { if (this.options.knownHelpersOnly) throw new k["default"]("You specified knownHelpersOnly, but used the unknown helper " + f, a); e.strict = !0, e.falsy = !0, this.accept(e), this.opcode("invokeHelper", d.length, e.original, n["default"].helpers.simpleId(e)) } }, PathExpression: function (a) { this.addDepth(a.depth), this.opcode("getContext", a.depth); var b = a.parts[0], c = n["default"].helpers.scopedId(a), d = !a.depth && !c && this.blockParamIndex(b); d ? this.opcode("lookupBlockParam", d, a.parts) : b ? a.data ? (this.options.data = !0, this.opcode("lookupData", a.depth, a.parts, a.strict)) : this.opcode("lookupOnContext", a.parts, a.falsy, a.strict, c) : this.opcode("pushContext") }, StringLiteral: function (a) { this.opcode("pushString", a.value) }, NumberLiteral: function (a) { this.opcode("pushLiteral", a.value) }, BooleanLiteral: function (a) { this.opcode("pushLiteral", a.value) }, UndefinedLiteral: function () { this.opcode("pushLiteral", "undefined") }, NullLiteral: function () { this.opcode("pushLiteral", "null") }, Hash: function (a) { var b = a.pairs, c = 0, d = b.length; for (this.opcode("pushHash") ; d > c; c++) this.pushParam(b[c].value); for (; c--;) this.opcode("assignToHash", b[c].key); this.opcode("popHash") }, opcode: function (a) { this.opcodes.push({ opcode: a, args: o.call(arguments, 1), loc: this.sourceNode[0].loc }) }, addDepth: function (a) { a && (this.useDepths = !0) }, classifySexpr: function (a) { var b = n["default"].helpers.simpleId(a.path), c = b && !!this.blockParamIndex(a.path.parts[0]), d = !c && n["default"].helpers.helperExpression(a), e = !c && (d || b); if (e && !d) { var f = a.path.parts[0], g = this.options; g.knownHelpers[f] ? d = !0 : g.knownHelpersOnly && (e = !1) } return d ? "helper" : e ? "ambiguous" : "simple" }, pushParams: function (a) { for (var b = 0, c = a.length; c > b; b++) this.pushParam(a[b]) }, pushParam: function (a) { var b = null != a.value ? a.value : a.original || ""; if (this.stringParams) b.replace && (b = b.replace(/^(\.?\.\/)*/g, "").replace(/\//g, ".")), a.depth && this.addDepth(a.depth), this.opcode("getContext", a.depth || 0), this.opcode("pushStringParam", b, a.type), "SubExpression" === a.type && this.accept(a); else { if (this.trackIds) { var c = void 0; if (!a.parts || n["default"].helpers.scopedId(a) || a.depth || (c = this.blockParamIndex(a.parts[0])), c) { var d = a.parts.slice(1).join("."); this.opcode("pushId", "BlockParam", c, d) } else b = a.original || b, b.replace && (b = b.replace(/^this(?:\.|$)/, "").replace(/^\.\//, "").replace(/^\.$/, "")), this.opcode("pushId", a.type, b) } this.accept(a) } }, setupFullMustacheParams: function (a, b, c, d) { var e = a.params; return this.pushParams(e), this.opcode("pushProgram", b), this.opcode("pushProgram", c), a.hash ? this.accept(a.hash) : this.opcode("emptyHash", d), e }, blockParamIndex: function (a) { for (var b = 0, c = this.options.blockParams.length; c > b; b++) { var d = this.options.blockParams[b], e = d && l.indexOf(d, a); if (d && e >= 0) return [b, e] } } } }, function (a, b, c) { "use strict"; function d(a) { this.value = a } function e() { } function f(a, b, c, d) { var e = b.popStack(), f = 0, g = c.length; for (a && g--; g > f; f++) e = b.nameLookup(e, c[f], d); return a ? [b.aliasable("container.strict"), "(", e, ", ", b.quotedString(c[f]), ")"] : e } var g = c(8)["default"]; b.__esModule = !0; var h = c(10), i = c(12), j = g(i), k = c(13), l = c(18), m = g(l); e.prototype = { nameLookup: function (a, b) { return e.isValidJavaScriptVariableName(b) ? [a, ".", b] : [a, "[", JSON.stringify(b), "]"] }, depthedLookup: function (a) { return [this.aliasable("container.lookup"), '(depths, "', a, '")'] }, compilerInfo: function () { var a = h.COMPILER_REVISION, b = h.REVISION_CHANGES[a]; return [a, b] }, appendToBuffer: function (a, b, c) { return k.isArray(a) || (a = [a]), a = this.source.wrap(a, b), this.environment.isSimple ? ["return ", a, ";"] : c ? ["buffer += ", a, ";"] : (a.appendToBuffer = !0, a) }, initializeBuffer: function () { return this.quotedString("") }, compile: function (a, b, c, d) { this.environment = a, this.options = b, this.stringParams = this.options.stringParams, this.trackIds = this.options.trackIds, this.precompile = !d, this.name = this.environment.name, this.isChild = !!c, this.context = c || { decorators: [], programs: [], environments: [] }, this.preamble(), this.stackSlot = 0, this.stackVars = [], this.aliases = {}, this.registers = { list: [] }, this.hashes = [], this.compileStack = [], this.inlineStack = [], this.blockParams = [], this.compileChildren(a, b), this.useDepths = this.useDepths || a.useDepths || a.useDecorators || this.options.compat, this.useBlockParams = this.useBlockParams || a.useBlockParams; var e = a.opcodes, f = void 0, g = void 0, h = void 0, i = void 0; for (h = 0, i = e.length; i > h; h++) f = e[h], this.source.currentLocation = f.loc, g = g || f.loc, this[f.opcode].apply(this, f.args); if (this.source.currentLocation = g, this.pushSource(""), this.stackSlot || this.inlineStack.length || this.compileStack.length) throw new j["default"]("Compile completed with content left on stack"); this.decorators.isEmpty() ? this.decorators = void 0 : (this.useDecorators = !0, this.decorators.prepend("var decorators = container.decorators;\n"), this.decorators.push("return fn;"), d ? this.decorators = Function.apply(this, ["fn", "props", "container", "depth0", "data", "blockParams", "depths", this.decorators.merge()]) : (this.decorators.prepend("function(fn, props, container, depth0, data, blockParams, depths) {\n"), this.decorators.push("}\n"), this.decorators = this.decorators.merge())); var k = this.createFunctionContext(d); if (this.isChild) return k; var l = { compiler: this.compilerInfo(), main: k }; this.decorators && (l.main_d = this.decorators, l.useDecorators = !0); var m = this.context, n = m.programs, o = m.decorators; for (h = 0, i = n.length; i > h; h++) n[h] && (l[h] = n[h], o[h] && (l[h + "_d"] = o[h], l.useDecorators = !0)); return this.environment.usePartial && (l.usePartial = !0), this.options.data && (l.useData = !0), this.useDepths && (l.useDepths = !0), this.useBlockParams && (l.useBlockParams = !0), this.options.compat && (l.compat = !0), d ? l.compilerOptions = this.options : (l.compiler = JSON.stringify(l.compiler), this.source.currentLocation = { start: { line: 1, column: 0 } }, l = this.objectLiteral(l), b.srcName ? (l = l.toStringWithSourceMap({ file: b.destName }), l.map = l.map && l.map.toString()) : l = l.toString()), l }, preamble: function () { this.lastContext = 0, this.source = new m["default"](this.options.srcName), this.decorators = new m["default"](this.options.srcName) }, createFunctionContext: function (a) { var b = "", c = this.stackVars.concat(this.registers.list); c.length > 0 && (b += ", " + c.join(", ")); var d = 0; for (var e in this.aliases) { var f = this.aliases[e]; this.aliases.hasOwnProperty(e) && f.children && f.referenceCount > 1 && (b += ", alias" + ++d + "=" + e, f.children[0] = "alias" + d) } var g = ["container", "depth0", "helpers", "partials", "data"]; (this.useBlockParams || this.useDepths) && g.push("blockParams"), this.useDepths && g.push("depths"); var h = this.mergeSource(b); return a ? (g.push(h), Function.apply(this, g)) : this.source.wrap(["function(", g.join(","), ") {\n  ", h, "}"]) }, mergeSource: function (a) { var b = this.environment.isSimple, c = !this.forceBuffer, d = void 0, e = void 0, f = void 0, g = void 0; return this.source.each(function (a) { a.appendToBuffer ? (f ? a.prepend("  + ") : f = a, g = a) : (f && (e ? f.prepend("buffer += ") : d = !0, g.add(";"), f = g = void 0), e = !0, b || (c = !1)) }), c ? f ? (f.prepend("return "), g.add(";")) : e || this.source.push('return "";') : (a += ", buffer = " + (d ? "" : this.initializeBuffer()), f ? (f.prepend("return buffer + "), g.add(";")) : this.source.push("return buffer;")), a && this.source.prepend("var " + a.substring(2) + (d ? "" : ";\n")), this.source.merge() }, blockValue: function (a) { var b = this.aliasable("helpers.blockHelperMissing"), c = [this.contextName(0)]; this.setupHelperArgs(a, 0, c); var d = this.popStack(); c.splice(1, 0, d), this.push(this.source.functionCall(b, "call", c)) }, ambiguousBlockValue: function () { var a = this.aliasable("helpers.blockHelperMissing"), b = [this.contextName(0)]; this.setupHelperArgs("", 0, b, !0), this.flushInline(); var c = this.topStack(); b.splice(1, 0, c), this.pushSource(["if (!", this.lastHelper, ") { ", c, " = ", this.source.functionCall(a, "call", b), "}"]) }, appendContent: function (a) { this.pendingContent ? a = this.pendingContent + a : this.pendingLocation = this.source.currentLocation, this.pendingContent = a }, append: function () { if (this.isInline()) this.replaceStack(function (a) { return [" != null ? ", a, ' : ""'] }), this.pushSource(this.appendToBuffer(this.popStack())); else { var a = this.popStack(); this.pushSource(["if (", a, " != null) { ", this.appendToBuffer(a, void 0, !0), " }"]), this.environment.isSimple && this.pushSource(["else { ", this.appendToBuffer("''", void 0, !0), " }"]) } }, appendEscaped: function () { this.pushSource(this.appendToBuffer([this.aliasable("container.escapeExpression"), "(", this.popStack(), ")"])) }, getContext: function (a) { this.lastContext = a }, pushContext: function () { this.pushStackLiteral(this.contextName(this.lastContext)) }, lookupOnContext: function (a, b, c, d) { var e = 0; d || !this.options.compat || this.lastContext ? this.pushContext() : this.push(this.depthedLookup(a[e++])), this.resolvePath("context", a, e, b, c) }, lookupBlockParam: function (a, b) { this.useBlockParams = !0, this.push(["blockParams[", a[0], "][", a[1], "]"]), this.resolvePath("context", b, 1) }, lookupData: function (a, b, c) { a ? this.pushStackLiteral("container.data(data, " + a + ")") : this.pushStackLiteral("data"), this.resolvePath("data", b, 0, !0, c) }, resolvePath: function (a, b, c, d, e) { var g = this; if (this.options.strict || this.options.assumeObjects) return void this.push(f(this.options.strict && e, this, b, a)); for (var h = b.length; h > c; c++) this.replaceStack(function (e) { var f = g.nameLookup(e, b[c], a); return d ? [" && ", f] : [" != null ? ", f, " : ", e] }) }, resolvePossibleLambda: function () { this.push([this.aliasable("container.lambda"), "(", this.popStack(), ", ", this.contextName(0), ")"]) }, pushStringParam: function (a, b) { this.pushContext(), this.pushString(b), "SubExpression" !== b && ("string" == typeof a ? this.pushString(a) : this.pushStackLiteral(a)) }, emptyHash: function (a) { this.trackIds && this.push("{}"), this.stringParams && (this.push("{}"), this.push("{}")), this.pushStackLiteral(a ? "undefined" : "{}") }, pushHash: function () { this.hash && this.hashes.push(this.hash), this.hash = { values: [], types: [], contexts: [], ids: [] } }, popHash: function () { var a = this.hash; this.hash = this.hashes.pop(), this.trackIds && this.push(this.objectLiteral(a.ids)), this.stringParams && (this.push(this.objectLiteral(a.contexts)), this.push(this.objectLiteral(a.types))), this.push(this.objectLiteral(a.values)) }, pushString: function (a) { this.pushStackLiteral(this.quotedString(a)) }, pushLiteral: function (a) { this.pushStackLiteral(a) }, pushProgram: function (a) { null != a ? this.pushStackLiteral(this.programExpression(a)) : this.pushStackLiteral(null) }, registerDecorator: function (a, b) { var c = this.nameLookup("decorators", b, "decorator"), d = this.setupHelperArgs(b, a); this.decorators.push(["fn = ", this.decorators.functionCall(c, "", ["fn", "props", "container", d]), " || fn;"]) }, invokeHelper: function (a, b, c) { var d = this.popStack(), e = this.setupHelper(a, b), f = c ? [e.name, " || "] : "", g = ["("].concat(f, d); this.options.strict || g.push(" || ", this.aliasable("helpers.helperMissing")), g.push(")"), this.push(this.source.functionCall(g, "call", e.callParams)) }, invokeKnownHelper: function (a, b) { var c = this.setupHelper(a, b); this.push(this.source.functionCall(c.name, "call", c.callParams)) }, invokeAmbiguous: function (a, b) { this.useRegister("helper"); var c = this.popStack(); this.emptyHash(); var d = this.setupHelper(0, a, b), e = this.lastHelper = this.nameLookup("helpers", a, "helper"), f = ["(", "(helper = ", e, " || ", c, ")"]; this.options.strict || (f[0] = "(helper = ", f.push(" != null ? helper : ", this.aliasable("helpers.helperMissing"))), this.push(["(", f, d.paramsInit ? ["),(", d.paramsInit] : [], "),", "(typeof helper === ", this.aliasable('"function"'), " ? ", this.source.functionCall("helper", "call", d.callParams), " : helper))"]) }, invokePartial: function (a, b, c) { var d = [], e = this.setupParams(b, 1, d); a && (b = this.popStack(), delete e.name), c && (e.indent = JSON.stringify(c)), e.helpers = "helpers", e.partials = "partials", e.decorators = "container.decorators", a ? d.unshift(b) : d.unshift(this.nameLookup("partials", b, "partial")), this.options.compat && (e.depths = "depths"), e = this.objectLiteral(e), d.push(e), this.push(this.source.functionCall("container.invokePartial", "", d)) }, assignToHash: function (a) { var b = this.popStack(), c = void 0, d = void 0, e = void 0; this.trackIds && (e = this.popStack()), this.stringParams && (d = this.popStack(), c = this.popStack()); var f = this.hash; c && (f.contexts[a] = c), d && (f.types[a] = d), e && (f.ids[a] = e), f.values[a] = b }, pushId: function (a, b, c) { "BlockParam" === a ? this.pushStackLiteral("blockParams[" + b[0] + "].path[" + b[1] + "]" + (c ? " + " + JSON.stringify("." + c) : "")) : "PathExpression" === a ? this.pushString(b) : "SubExpression" === a ? this.pushStackLiteral("true") : this.pushStackLiteral("null") }, compiler: e, compileChildren: function (a, b) { for (var c = a.children, d = void 0, e = void 0, f = 0, g = c.length; g > f; f++) { d = c[f], e = new this.compiler; var h = this.matchExistingProgram(d); null == h ? (this.context.programs.push(""), h = this.context.programs.length, d.index = h, d.name = "program" + h, this.context.programs[h] = e.compile(d, b, this.context, !this.precompile), this.context.decorators[h] = e.decorators, this.context.environments[h] = d, this.useDepths = this.useDepths || e.useDepths, this.useBlockParams = this.useBlockParams || e.useBlockParams) : (d.index = h, d.name = "program" + h, this.useDepths = this.useDepths || d.useDepths, this.useBlockParams = this.useBlockParams || d.useBlockParams) } }, matchExistingProgram: function (a) { for (var b = 0, c = this.context.environments.length; c > b; b++) { var d = this.context.environments[b]; if (d && d.equals(a)) return b } }, programExpression: function (a) { var b = this.environment.children[a], c = [b.index, "data", b.blockParams]; return (this.useBlockParams || this.useDepths) && c.push("blockParams"), this.useDepths && c.push("depths"), "container.program(" + c.join(", ") + ")" }, useRegister: function (a) { this.registers[a] || (this.registers[a] = !0, this.registers.list.push(a)) }, push: function (a) { return a instanceof d || (a = this.source.wrap(a)), this.inlineStack.push(a), a }, pushStackLiteral: function (a) { this.push(new d(a)) }, pushSource: function (a) { this.pendingContent && (this.source.push(this.appendToBuffer(this.source.quotedString(this.pendingContent), this.pendingLocation)), this.pendingContent = void 0), a && this.source.push(a) }, replaceStack: function (a) { var b = ["("], c = void 0, e = void 0, f = void 0; if (!this.isInline()) throw new j["default"]("replaceStack on non-inline"); var g = this.popStack(!0); if (g instanceof d) c = [g.value], b = ["(", c], f = !0; else { e = !0; var h = this.incrStack(); b = ["((", this.push(h), " = ", g, ")"], c = this.topStack() } var i = a.call(this, c); f || this.popStack(), e && this.stackSlot--, this.push(b.concat(i, ")")) }, incrStack: function () { return this.stackSlot++, this.stackSlot > this.stackVars.length && this.stackVars.push("stack" + this.stackSlot), this.topStackName() }, topStackName: function () { return "stack" + this.stackSlot }, flushInline: function () { var a = this.inlineStack; this.inlineStack = []; for (var b = 0, c = a.length; c > b; b++) { var e = a[b]; if (e instanceof d) this.compileStack.push(e); else { var f = this.incrStack(); this.pushSource([f, " = ", e, ";"]), this.compileStack.push(f) } } }, isInline: function () { return this.inlineStack.length }, popStack: function (a) { var b = this.isInline(), c = (b ? this.inlineStack : this.compileStack).pop(); if (!a && c instanceof d) return c.value; if (!b) { if (!this.stackSlot) throw new j["default"]("Invalid stack pop"); this.stackSlot-- } return c }, topStack: function () { var a = this.isInline() ? this.inlineStack : this.compileStack, b = a[a.length - 1]; return b instanceof d ? b.value : b }, contextName: function (a) { return this.useDepths && a ? "depths[" + a + "]" : "depth" + a }, quotedString: function (a) { return this.source.quotedString(a) }, objectLiteral: function (a) { return this.source.objectLiteral(a) }, aliasable: function (a) { var b = this.aliases[a]; return b ? (b.referenceCount++, b) : (b = this.aliases[a] = this.source.wrap(a), b.aliasable = !0, b.referenceCount = 1, b) }, setupHelper: function (a, b, c) { var d = [], e = this.setupHelperArgs(b, a, d, c), f = this.nameLookup("helpers", b, "helper"), g = this.aliasable(this.contextName(0) + " != null ? " + this.contextName(0) + " : {}"); return { params: d, paramsInit: e, name: f, callParams: [g].concat(d) } }, setupParams: function (a, b, c) { var d = {}, e = [], f = [], g = [], h = !c, i = void 0; h && (c = []), d.name = this.quotedString(a), d.hash = this.popStack(), this.trackIds && (d.hashIds = this.popStack()), this.stringParams && (d.hashTypes = this.popStack(), d.hashContexts = this.popStack()); var j = this.popStack(), k = this.popStack(); (k || j) && (d.fn = k || "container.noop", d.inverse = j || "container.noop"); for (var l = b; l--;) i = this.popStack(), c[l] = i, this.trackIds && (g[l] = this.popStack()), this.stringParams && (f[l] = this.popStack(), e[l] = this.popStack()); return h && (d.args = this.source.generateArray(c)), this.trackIds && (d.ids = this.source.generateArray(g)), this.stringParams && (d.types = this.source.generateArray(f), d.contexts = this.source.generateArray(e)), this.options.data && (d.data = "data"), this.useBlockParams && (d.blockParams = "blockParams"), d }, setupHelperArgs: function (a, b, c, d) { var e = this.setupParams(a, b, c); return e = this.objectLiteral(e), d ? (this.useRegister("options"), c.push("options"), ["options=", e]) : c ? (c.push(e), "") : e } }, function () { for (var a = "break else new var case finally return void catch for switch while continue function this with default if throw delete in try do instanceof typeof abstract enum int short boolean export interface static byte extends long super char final native synchronized class float package throws const goto private transient debugger implements protected volatile double import public let yield await null true false".split(" "), b = e.RESERVED_WORDS = {}, c = 0, d = a.length; d > c; c++) b[a[c]] = !0 }(), e.isValidJavaScriptVariableName = function (a) { return !e.RESERVED_WORDS[a] && /^[a-zA-Z_$][0-9a-zA-Z_$]*$/.test(a) }, b["default"] = e, a.exports = b["default"] }, function (a, b, c) { "use strict"; function d() { this.parents = [] } function e(a) { this.acceptRequired(a, "path"), this.acceptArray(a.params), this.acceptKey(a, "hash") } function f(a) { e.call(this, a), this.acceptKey(a, "program"), this.acceptKey(a, "inverse") } function g(a) { this.acceptRequired(a, "name"), this.acceptArray(a.params), this.acceptKey(a, "hash") } var h = c(8)["default"]; b.__esModule = !0; var i = c(12), j = h(i); d.prototype = { constructor: d, mutating: !1, acceptKey: function (a, b) { var c = this.accept(a[b]); if (this.mutating) { if (c && !d.prototype[c.type]) throw new j["default"]('Unexpected node type "' + c.type + '" found when accepting ' + b + " on " + a.type); a[b] = c } }, acceptRequired: function (a, b) { if (this.acceptKey(a, b), !a[b]) throw new j["default"](a.type + " requires " + b) }, acceptArray: function (a) { for (var b = 0, c = a.length; c > b; b++) this.acceptKey(a, b), a[b] || (a.splice(b, 1), b--, c--) }, accept: function (a) { if (a) { if (!this[a.type]) throw new j["default"]("Unknown type: " + a.type, a); this.current && this.parents.unshift(this.current), this.current = a; var b = this[a.type](a); return this.current = this.parents.shift(), !this.mutating || b ? b : b !== !1 ? a : void 0 } }, Program: function (a) { this.acceptArray(a.body) }, MustacheStatement: e, Decorator: e, BlockStatement: f, DecoratorBlock: f, PartialStatement: g, PartialBlockStatement: function (a) { g.call(this, a), this.acceptKey(a, "program") }, ContentStatement: function () { }, CommentStatement: function () { }, SubExpression: e, PathExpression: function () { }, StringLiteral: function () { }, NumberLiteral: function () { }, BooleanLiteral: function () { }, UndefinedLiteral: function () { }, NullLiteral: function () { }, Hash: function (a) { this.acceptArray(a.pairs) }, HashPair: function (a) { this.acceptRequired(a, "value") } }, b["default"] = d, a.exports = b["default"] }, function (a, b, c) { (function (c) { "use strict"; b.__esModule = !0, b["default"] = function (a) { var b = "undefined" != typeof c ? c : window, d = b.Handlebars; a.noConflict = function () { b.Handlebars === a && (b.Handlebars = d) } }, a.exports = b["default"] }).call(b, function () { return this }()) }, function (a, b, c) { "use strict"; b["default"] = function (a) { return a && a.__esModule ? a : { "default": a } }, b.__esModule = !0 }, function (a, b, c) { "use strict"; b["default"] = function (a) { if (a && a.__esModule) return a; var b = {}; if (null != a) for (var c in a) Object.prototype.hasOwnProperty.call(a, c) && (b[c] = a[c]); return b["default"] = a, b }, b.__esModule = !0 }, function (a, b, c) { "use strict"; function d(a, b, c) { this.helpers = a || {}, this.partials = b || {}, this.decorators = c || {}, i.registerDefaultHelpers(this), j.registerDefaultDecorators(this) } var e = c(8)["default"]; b.__esModule = !0, b.HandlebarsEnvironment = d; var f = c(13), g = c(12), h = e(g), i = c(19), j = c(20), k = c(21), l = e(k), m = "4.0.4"; b.VERSION = m; var n = 7; b.COMPILER_REVISION = n; var o = { 1: "<= 1.0.rc.2", 2: "== 1.0.0-rc.3", 3: "== 1.0.0-rc.4", 4: "== 1.x.x", 5: "== 2.0.0-alpha.x", 6: ">= 2.0.0-beta.1", 7: ">= 4.0.0" }; b.REVISION_CHANGES = o; var p = "[object Object]"; d.prototype = { constructor: d, logger: l["default"], log: l["default"].log, registerHelper: function (a, b) { if (f.toString.call(a) === p) { if (b) throw new h["default"]("Arg not supported with multiple helpers"); f.extend(this.helpers, a) } else this.helpers[a] = b }, unregisterHelper: function (a) { delete this.helpers[a] }, registerPartial: function (a, b) { if (f.toString.call(a) === p) f.extend(this.partials, a); else { if ("undefined" == typeof b) throw new h["default"]('Attempting to register a partial called "' + a + '" as undefined'); this.partials[a] = b } }, unregisterPartial: function (a) { delete this.partials[a] }, registerDecorator: function (a, b) { if (f.toString.call(a) === p) { if (b) throw new h["default"]("Arg not supported with multiple decorators"); f.extend(this.decorators, a) } else this.decorators[a] = b }, unregisterDecorator: function (a) { delete this.decorators[a] } }; var q = l["default"].log; b.log = q, b.createFrame = f.createFrame, b.logger = l["default"] }, function (a, b, c) { "use strict"; function d(a) { this.string = a } b.__esModule = !0, d.prototype.toString = d.prototype.toHTML = function () { return "" + this.string }, b["default"] = d, a.exports = b["default"] }, function (a, b, c) { "use strict"; function d(a, b) { var c = b && b.loc, f = void 0, g = void 0; c && (f = c.start.line, g = c.start.column, a += " - " + f + ":" + g); for (var h = Error.prototype.constructor.call(this, a), i = 0; i < e.length; i++) this[e[i]] = h[e[i]]; Error.captureStackTrace && Error.captureStackTrace(this, d), c && (this.lineNumber = f, this.column = g) } b.__esModule = !0; var e = ["description", "fileName", "lineNumber", "message", "name", "number", "stack"]; d.prototype = new Error, b["default"] = d, a.exports = b["default"] }, function (a, b, c) { "use strict"; function d(a) { return l[a] } function e(a) { for (var b = 1; b < arguments.length; b++) for (var c in arguments[b]) Object.prototype.hasOwnProperty.call(arguments[b], c) && (a[c] = arguments[b][c]); return a } function f(a, b) { for (var c = 0, d = a.length; d > c; c++) if (a[c] === b) return c; return -1 } function g(a) { if ("string" != typeof a) { if (a && a.toHTML) return a.toHTML(); if (null == a) return ""; if (!a) return a + ""; a = "" + a } return n.test(a) ? a.replace(m, d) : a } function h(a) { return a || 0 === a ? q(a) && 0 === a.length ? !0 : !1 : !0 } function i(a) { var b = e({}, a); return b._parent = a, b } function j(a, b) { return a.path = b, a } function k(a, b) { return (a ? a + "." : "") + b } b.__esModule = !0, b.extend = e, b.indexOf = f, b.escapeExpression = g, b.isEmpty = h, b.createFrame = i, b.blockParams = j, b.appendContextPath = k; var l = { "&": "&amp;", "<": "&lt;", ">": "&gt;", '"': "&quot;", "'": "&#x27;", "`": "&#x60;", "=": "&#x3D;" }, m = /[&<>"'`=]/g, n = /[&<>"'`=]/, o = Object.prototype.toString; b.toString = o; var p = function (a) { return "function" == typeof a }; p(/x/) && (b.isFunction = p = function (a) { return "function" == typeof a && "[object Function]" === o.call(a) }), b.isFunction = p; var q = Array.isArray || function (a) { return a && "object" == typeof a ? "[object Array]" === o.call(a) : !1 }; b.isArray = q }, function (a, b, c) {
        "use strict"; function d(a) { var b = a && a[0] || 1, c = r.COMPILER_REVISION; if (b !== c) { if (c > b) { var d = r.REVISION_CHANGES[c], e = r.REVISION_CHANGES[b]; throw new q["default"]("Template was precompiled with an older version of Handlebars than the current runtime. Please update your precompiler to a newer version (" + d + ") or downgrade your runtime to an older version (" + e + ").") } throw new q["default"]("Template was precompiled with a newer version of Handlebars than the current runtime. Please update your runtime to a newer version (" + a[1] + ").") } } function e(a, b) {
            function c(c, d, e) { e.hash && (d = o.extend({}, d, e.hash), e.ids && (e.ids[0] = !0)), c = b.VM.resolvePartial.call(this, c, d, e); var f = b.VM.invokePartial.call(this, c, d, e); if (null == f && b.compile && (e.partials[e.name] = b.compile(c, a.compilerOptions, b), f = e.partials[e.name](d, e)), null != f) { if (e.indent) { for (var g = f.split("\n"), h = 0, i = g.length; i > h && (g[h] || h + 1 !== i) ; h++) g[h] = e.indent + g[h]; f = g.join("\n") } return f } throw new q["default"]("The partial " + e.name + " could not be compiled when running in runtime-only mode") } function d(b) { function c(b) { return "" + a.main(e, b, e.helpers, e.partials, g, i, h) } var f = arguments.length <= 1 || void 0 === arguments[1] ? {} : arguments[1], g = f.data; d._setup(f), !f.partial && a.useData && (g = j(b, g)); var h = void 0, i = a.useBlockParams ? [] : void 0; return a.useDepths && (h = f.depths ? b !== f.depths[0] ? [b].concat(f.depths) : f.depths : [b]), (c = k(a.main, c, e, f.depths || [], g, i))(b, f) } if (!b) throw new q["default"]("No environment passed to template"); if (!a || !a.main) throw new q["default"]("Unknown template object: " + typeof a); a.main.decorator = a.main_d, b.VM.checkRevision(a.compiler); var e = {
                strict: function (a, b) { if (!(b in a)) throw new q["default"]('"' + b + '" not defined in ' + a); return a[b] }, lookup: function (a, b) { for (var c = a.length, d = 0; c > d; d++) if (a[d] && null != a[d][b]) return a[d][b] }, lambda: function (a, b) { return "function" == typeof a ? a.call(b) : a }, escapeExpression: o.escapeExpression, invokePartial: c, fn: function (b) { var c = a[b]; return c.decorator = a[b + "_d"], c }, programs: [], program: function (a, b, c, d, e) { var g = this.programs[a], h = this.fn(a); return b || e || d || c ? g = f(this, a, h, b, c, d, e) : g || (g = this.programs[a] = f(this, a, h)), g }, data: function (a, b) {
                    for (; a && b--;) a = a._parent;
                    return a
                }, merge: function (a, b) { var c = a || b; return a && b && a !== b && (c = o.extend({}, b, a)), c }, noop: b.VM.noop, compilerInfo: a.compiler
            }; return d.isTop = !0, d._setup = function (c) { c.partial ? (e.helpers = c.helpers, e.partials = c.partials, e.decorators = c.decorators) : (e.helpers = e.merge(c.helpers, b.helpers), a.usePartial && (e.partials = e.merge(c.partials, b.partials)), (a.usePartial || a.useDecorators) && (e.decorators = e.merge(c.decorators, b.decorators))) }, d._child = function (b, c, d, g) { if (a.useBlockParams && !d) throw new q["default"]("must pass block params"); if (a.useDepths && !g) throw new q["default"]("must pass parent depths"); return f(e, b, a[b], c, 0, d, g) }, d
        } function f(a, b, c, d, e, f, g) { function h(b) { var e = arguments.length <= 1 || void 0 === arguments[1] ? {} : arguments[1], h = g; return g && b !== g[0] && (h = [b].concat(g)), c(a, b, a.helpers, a.partials, e.data || d, f && [e.blockParams].concat(f), h) } return h = k(c, h, a, g, d, f), h.program = b, h.depth = g ? g.length : 0, h.blockParams = e || 0, h } function g(a, b, c) { return a ? a.call || c.name || (c.name = a, a = c.partials[a]) : a = "@partial-block" === c.name ? c.data["partial-block"] : c.partials[c.name], a } function h(a, b, c) { c.partial = !0, c.ids && (c.data.contextPath = c.ids[0] || c.data.contextPath); var d = void 0; if (c.fn && c.fn !== i && (c.data = r.createFrame(c.data), d = c.data["partial-block"] = c.fn, d.partials && (c.partials = o.extend({}, c.partials, d.partials))), void 0 === a && d && (a = d), void 0 === a) throw new q["default"]("The partial " + c.name + " could not be found"); return a instanceof Function ? a(b, c) : void 0 } function i() { return "" } function j(a, b) { return b && "root" in b || (b = b ? r.createFrame(b) : {}, b.root = a), b } function k(a, b, c, d, e, f) { if (a.decorator) { var g = {}; b = a.decorator(b, g, c, d && d[0], e, f, d), o.extend(b, g) } return b } var l = c(9)["default"], m = c(8)["default"]; b.__esModule = !0, b.checkRevision = d, b.template = e, b.wrapProgram = f, b.resolvePartial = g, b.invokePartial = h, b.noop = i; var n = c(13), o = l(n), p = c(12), q = m(p), r = c(10)
    }, function (a, b, c) { "use strict"; var d = function () { function a() { this.yy = {} } var b = { trace: function () { }, yy: {}, symbols_: { error: 2, root: 3, program: 4, EOF: 5, program_repetition0: 6, statement: 7, mustache: 8, block: 9, rawBlock: 10, partial: 11, partialBlock: 12, content: 13, COMMENT: 14, CONTENT: 15, openRawBlock: 16, rawBlock_repetition_plus0: 17, END_RAW_BLOCK: 18, OPEN_RAW_BLOCK: 19, helperName: 20, openRawBlock_repetition0: 21, openRawBlock_option0: 22, CLOSE_RAW_BLOCK: 23, openBlock: 24, block_option0: 25, closeBlock: 26, openInverse: 27, block_option1: 28, OPEN_BLOCK: 29, openBlock_repetition0: 30, openBlock_option0: 31, openBlock_option1: 32, CLOSE: 33, OPEN_INVERSE: 34, openInverse_repetition0: 35, openInverse_option0: 36, openInverse_option1: 37, openInverseChain: 38, OPEN_INVERSE_CHAIN: 39, openInverseChain_repetition0: 40, openInverseChain_option0: 41, openInverseChain_option1: 42, inverseAndProgram: 43, INVERSE: 44, inverseChain: 45, inverseChain_option0: 46, OPEN_ENDBLOCK: 47, OPEN: 48, mustache_repetition0: 49, mustache_option0: 50, OPEN_UNESCAPED: 51, mustache_repetition1: 52, mustache_option1: 53, CLOSE_UNESCAPED: 54, OPEN_PARTIAL: 55, partialName: 56, partial_repetition0: 57, partial_option0: 58, openPartialBlock: 59, OPEN_PARTIAL_BLOCK: 60, openPartialBlock_repetition0: 61, openPartialBlock_option0: 62, param: 63, sexpr: 64, OPEN_SEXPR: 65, sexpr_repetition0: 66, sexpr_option0: 67, CLOSE_SEXPR: 68, hash: 69, hash_repetition_plus0: 70, hashSegment: 71, ID: 72, EQUALS: 73, blockParams: 74, OPEN_BLOCK_PARAMS: 75, blockParams_repetition_plus0: 76, CLOSE_BLOCK_PARAMS: 77, path: 78, dataName: 79, STRING: 80, NUMBER: 81, BOOLEAN: 82, UNDEFINED: 83, NULL: 84, DATA: 85, pathSegments: 86, SEP: 87, $accept: 0, $end: 1 }, terminals_: { 2: "error", 5: "EOF", 14: "COMMENT", 15: "CONTENT", 18: "END_RAW_BLOCK", 19: "OPEN_RAW_BLOCK", 23: "CLOSE_RAW_BLOCK", 29: "OPEN_BLOCK", 33: "CLOSE", 34: "OPEN_INVERSE", 39: "OPEN_INVERSE_CHAIN", 44: "INVERSE", 47: "OPEN_ENDBLOCK", 48: "OPEN", 51: "OPEN_UNESCAPED", 54: "CLOSE_UNESCAPED", 55: "OPEN_PARTIAL", 60: "OPEN_PARTIAL_BLOCK", 65: "OPEN_SEXPR", 68: "CLOSE_SEXPR", 72: "ID", 73: "EQUALS", 75: "OPEN_BLOCK_PARAMS", 77: "CLOSE_BLOCK_PARAMS", 80: "STRING", 81: "NUMBER", 82: "BOOLEAN", 83: "UNDEFINED", 84: "NULL", 85: "DATA", 87: "SEP" }, productions_: [0, [3, 2], [4, 1], [7, 1], [7, 1], [7, 1], [7, 1], [7, 1], [7, 1], [7, 1], [13, 1], [10, 3], [16, 5], [9, 4], [9, 4], [24, 6], [27, 6], [38, 6], [43, 2], [45, 3], [45, 1], [26, 3], [8, 5], [8, 5], [11, 5], [12, 3], [59, 5], [63, 1], [63, 1], [64, 5], [69, 1], [71, 3], [74, 3], [20, 1], [20, 1], [20, 1], [20, 1], [20, 1], [20, 1], [20, 1], [56, 1], [56, 1], [79, 2], [78, 1], [86, 3], [86, 1], [6, 0], [6, 2], [17, 1], [17, 2], [21, 0], [21, 2], [22, 0], [22, 1], [25, 0], [25, 1], [28, 0], [28, 1], [30, 0], [30, 2], [31, 0], [31, 1], [32, 0], [32, 1], [35, 0], [35, 2], [36, 0], [36, 1], [37, 0], [37, 1], [40, 0], [40, 2], [41, 0], [41, 1], [42, 0], [42, 1], [46, 0], [46, 1], [49, 0], [49, 2], [50, 0], [50, 1], [52, 0], [52, 2], [53, 0], [53, 1], [57, 0], [57, 2], [58, 0], [58, 1], [61, 0], [61, 2], [62, 0], [62, 1], [66, 0], [66, 2], [67, 0], [67, 1], [70, 1], [70, 2], [76, 1], [76, 2]], performAction: function (a, b, c, d, e, f, g) { var h = f.length - 1; switch (e) { case 1: return f[h - 1]; case 2: this.$ = d.prepareProgram(f[h]); break; case 3: this.$ = f[h]; break; case 4: this.$ = f[h]; break; case 5: this.$ = f[h]; break; case 6: this.$ = f[h]; break; case 7: this.$ = f[h]; break; case 8: this.$ = f[h]; break; case 9: this.$ = { type: "CommentStatement", value: d.stripComment(f[h]), strip: d.stripFlags(f[h], f[h]), loc: d.locInfo(this._$) }; break; case 10: this.$ = { type: "ContentStatement", original: f[h], value: f[h], loc: d.locInfo(this._$) }; break; case 11: this.$ = d.prepareRawBlock(f[h - 2], f[h - 1], f[h], this._$); break; case 12: this.$ = { path: f[h - 3], params: f[h - 2], hash: f[h - 1] }; break; case 13: this.$ = d.prepareBlock(f[h - 3], f[h - 2], f[h - 1], f[h], !1, this._$); break; case 14: this.$ = d.prepareBlock(f[h - 3], f[h - 2], f[h - 1], f[h], !0, this._$); break; case 15: this.$ = { open: f[h - 5], path: f[h - 4], params: f[h - 3], hash: f[h - 2], blockParams: f[h - 1], strip: d.stripFlags(f[h - 5], f[h]) }; break; case 16: this.$ = { path: f[h - 4], params: f[h - 3], hash: f[h - 2], blockParams: f[h - 1], strip: d.stripFlags(f[h - 5], f[h]) }; break; case 17: this.$ = { path: f[h - 4], params: f[h - 3], hash: f[h - 2], blockParams: f[h - 1], strip: d.stripFlags(f[h - 5], f[h]) }; break; case 18: this.$ = { strip: d.stripFlags(f[h - 1], f[h - 1]), program: f[h] }; break; case 19: var i = d.prepareBlock(f[h - 2], f[h - 1], f[h], f[h], !1, this._$), j = d.prepareProgram([i], f[h - 1].loc); j.chained = !0, this.$ = { strip: f[h - 2].strip, program: j, chain: !0 }; break; case 20: this.$ = f[h]; break; case 21: this.$ = { path: f[h - 1], strip: d.stripFlags(f[h - 2], f[h]) }; break; case 22: this.$ = d.prepareMustache(f[h - 3], f[h - 2], f[h - 1], f[h - 4], d.stripFlags(f[h - 4], f[h]), this._$); break; case 23: this.$ = d.prepareMustache(f[h - 3], f[h - 2], f[h - 1], f[h - 4], d.stripFlags(f[h - 4], f[h]), this._$); break; case 24: this.$ = { type: "PartialStatement", name: f[h - 3], params: f[h - 2], hash: f[h - 1], indent: "", strip: d.stripFlags(f[h - 4], f[h]), loc: d.locInfo(this._$) }; break; case 25: this.$ = d.preparePartialBlock(f[h - 2], f[h - 1], f[h], this._$); break; case 26: this.$ = { path: f[h - 3], params: f[h - 2], hash: f[h - 1], strip: d.stripFlags(f[h - 4], f[h]) }; break; case 27: this.$ = f[h]; break; case 28: this.$ = f[h]; break; case 29: this.$ = { type: "SubExpression", path: f[h - 3], params: f[h - 2], hash: f[h - 1], loc: d.locInfo(this._$) }; break; case 30: this.$ = { type: "Hash", pairs: f[h], loc: d.locInfo(this._$) }; break; case 31: this.$ = { type: "HashPair", key: d.id(f[h - 2]), value: f[h], loc: d.locInfo(this._$) }; break; case 32: this.$ = d.id(f[h - 1]); break; case 33: this.$ = f[h]; break; case 34: this.$ = f[h]; break; case 35: this.$ = { type: "StringLiteral", value: f[h], original: f[h], loc: d.locInfo(this._$) }; break; case 36: this.$ = { type: "NumberLiteral", value: Number(f[h]), original: Number(f[h]), loc: d.locInfo(this._$) }; break; case 37: this.$ = { type: "BooleanLiteral", value: "true" === f[h], original: "true" === f[h], loc: d.locInfo(this._$) }; break; case 38: this.$ = { type: "UndefinedLiteral", original: void 0, value: void 0, loc: d.locInfo(this._$) }; break; case 39: this.$ = { type: "NullLiteral", original: null, value: null, loc: d.locInfo(this._$) }; break; case 40: this.$ = f[h]; break; case 41: this.$ = f[h]; break; case 42: this.$ = d.preparePath(!0, f[h], this._$); break; case 43: this.$ = d.preparePath(!1, f[h], this._$); break; case 44: f[h - 2].push({ part: d.id(f[h]), original: f[h], separator: f[h - 1] }), this.$ = f[h - 2]; break; case 45: this.$ = [{ part: d.id(f[h]), original: f[h] }]; break; case 46: this.$ = []; break; case 47: f[h - 1].push(f[h]); break; case 48: this.$ = [f[h]]; break; case 49: f[h - 1].push(f[h]); break; case 50: this.$ = []; break; case 51: f[h - 1].push(f[h]); break; case 58: this.$ = []; break; case 59: f[h - 1].push(f[h]); break; case 64: this.$ = []; break; case 65: f[h - 1].push(f[h]); break; case 70: this.$ = []; break; case 71: f[h - 1].push(f[h]); break; case 78: this.$ = []; break; case 79: f[h - 1].push(f[h]); break; case 82: this.$ = []; break; case 83: f[h - 1].push(f[h]); break; case 86: this.$ = []; break; case 87: f[h - 1].push(f[h]); break; case 90: this.$ = []; break; case 91: f[h - 1].push(f[h]); break; case 94: this.$ = []; break; case 95: f[h - 1].push(f[h]); break; case 98: this.$ = [f[h]]; break; case 99: f[h - 1].push(f[h]); break; case 100: this.$ = [f[h]]; break; case 101: f[h - 1].push(f[h]) } }, table: [{ 3: 1, 4: 2, 5: [2, 46], 6: 3, 14: [2, 46], 15: [2, 46], 19: [2, 46], 29: [2, 46], 34: [2, 46], 48: [2, 46], 51: [2, 46], 55: [2, 46], 60: [2, 46] }, { 1: [3] }, { 5: [1, 4] }, { 5: [2, 2], 7: 5, 8: 6, 9: 7, 10: 8, 11: 9, 12: 10, 13: 11, 14: [1, 12], 15: [1, 20], 16: 17, 19: [1, 23], 24: 15, 27: 16, 29: [1, 21], 34: [1, 22], 39: [2, 2], 44: [2, 2], 47: [2, 2], 48: [1, 13], 51: [1, 14], 55: [1, 18], 59: 19, 60: [1, 24] }, { 1: [2, 1] }, { 5: [2, 47], 14: [2, 47], 15: [2, 47], 19: [2, 47], 29: [2, 47], 34: [2, 47], 39: [2, 47], 44: [2, 47], 47: [2, 47], 48: [2, 47], 51: [2, 47], 55: [2, 47], 60: [2, 47] }, { 5: [2, 3], 14: [2, 3], 15: [2, 3], 19: [2, 3], 29: [2, 3], 34: [2, 3], 39: [2, 3], 44: [2, 3], 47: [2, 3], 48: [2, 3], 51: [2, 3], 55: [2, 3], 60: [2, 3] }, { 5: [2, 4], 14: [2, 4], 15: [2, 4], 19: [2, 4], 29: [2, 4], 34: [2, 4], 39: [2, 4], 44: [2, 4], 47: [2, 4], 48: [2, 4], 51: [2, 4], 55: [2, 4], 60: [2, 4] }, { 5: [2, 5], 14: [2, 5], 15: [2, 5], 19: [2, 5], 29: [2, 5], 34: [2, 5], 39: [2, 5], 44: [2, 5], 47: [2, 5], 48: [2, 5], 51: [2, 5], 55: [2, 5], 60: [2, 5] }, { 5: [2, 6], 14: [2, 6], 15: [2, 6], 19: [2, 6], 29: [2, 6], 34: [2, 6], 39: [2, 6], 44: [2, 6], 47: [2, 6], 48: [2, 6], 51: [2, 6], 55: [2, 6], 60: [2, 6] }, { 5: [2, 7], 14: [2, 7], 15: [2, 7], 19: [2, 7], 29: [2, 7], 34: [2, 7], 39: [2, 7], 44: [2, 7], 47: [2, 7], 48: [2, 7], 51: [2, 7], 55: [2, 7], 60: [2, 7] }, { 5: [2, 8], 14: [2, 8], 15: [2, 8], 19: [2, 8], 29: [2, 8], 34: [2, 8], 39: [2, 8], 44: [2, 8], 47: [2, 8], 48: [2, 8], 51: [2, 8], 55: [2, 8], 60: [2, 8] }, { 5: [2, 9], 14: [2, 9], 15: [2, 9], 19: [2, 9], 29: [2, 9], 34: [2, 9], 39: [2, 9], 44: [2, 9], 47: [2, 9], 48: [2, 9], 51: [2, 9], 55: [2, 9], 60: [2, 9] }, { 20: 25, 72: [1, 35], 78: 26, 79: 27, 80: [1, 28], 81: [1, 29], 82: [1, 30], 83: [1, 31], 84: [1, 32], 85: [1, 34], 86: 33 }, { 20: 36, 72: [1, 35], 78: 26, 79: 27, 80: [1, 28], 81: [1, 29], 82: [1, 30], 83: [1, 31], 84: [1, 32], 85: [1, 34], 86: 33 }, { 4: 37, 6: 3, 14: [2, 46], 15: [2, 46], 19: [2, 46], 29: [2, 46], 34: [2, 46], 39: [2, 46], 44: [2, 46], 47: [2, 46], 48: [2, 46], 51: [2, 46], 55: [2, 46], 60: [2, 46] }, { 4: 38, 6: 3, 14: [2, 46], 15: [2, 46], 19: [2, 46], 29: [2, 46], 34: [2, 46], 44: [2, 46], 47: [2, 46], 48: [2, 46], 51: [2, 46], 55: [2, 46], 60: [2, 46] }, { 13: 40, 15: [1, 20], 17: 39 }, { 20: 42, 56: 41, 64: 43, 65: [1, 44], 72: [1, 35], 78: 26, 79: 27, 80: [1, 28], 81: [1, 29], 82: [1, 30], 83: [1, 31], 84: [1, 32], 85: [1, 34], 86: 33 }, { 4: 45, 6: 3, 14: [2, 46], 15: [2, 46], 19: [2, 46], 29: [2, 46], 34: [2, 46], 47: [2, 46], 48: [2, 46], 51: [2, 46], 55: [2, 46], 60: [2, 46] }, { 5: [2, 10], 14: [2, 10], 15: [2, 10], 18: [2, 10], 19: [2, 10], 29: [2, 10], 34: [2, 10], 39: [2, 10], 44: [2, 10], 47: [2, 10], 48: [2, 10], 51: [2, 10], 55: [2, 10], 60: [2, 10] }, { 20: 46, 72: [1, 35], 78: 26, 79: 27, 80: [1, 28], 81: [1, 29], 82: [1, 30], 83: [1, 31], 84: [1, 32], 85: [1, 34], 86: 33 }, { 20: 47, 72: [1, 35], 78: 26, 79: 27, 80: [1, 28], 81: [1, 29], 82: [1, 30], 83: [1, 31], 84: [1, 32], 85: [1, 34], 86: 33 }, { 20: 48, 72: [1, 35], 78: 26, 79: 27, 80: [1, 28], 81: [1, 29], 82: [1, 30], 83: [1, 31], 84: [1, 32], 85: [1, 34], 86: 33 }, { 20: 42, 56: 49, 64: 43, 65: [1, 44], 72: [1, 35], 78: 26, 79: 27, 80: [1, 28], 81: [1, 29], 82: [1, 30], 83: [1, 31], 84: [1, 32], 85: [1, 34], 86: 33 }, { 33: [2, 78], 49: 50, 65: [2, 78], 72: [2, 78], 80: [2, 78], 81: [2, 78], 82: [2, 78], 83: [2, 78], 84: [2, 78], 85: [2, 78] }, { 23: [2, 33], 33: [2, 33], 54: [2, 33], 65: [2, 33], 68: [2, 33], 72: [2, 33], 75: [2, 33], 80: [2, 33], 81: [2, 33], 82: [2, 33], 83: [2, 33], 84: [2, 33], 85: [2, 33] }, { 23: [2, 34], 33: [2, 34], 54: [2, 34], 65: [2, 34], 68: [2, 34], 72: [2, 34], 75: [2, 34], 80: [2, 34], 81: [2, 34], 82: [2, 34], 83: [2, 34], 84: [2, 34], 85: [2, 34] }, { 23: [2, 35], 33: [2, 35], 54: [2, 35], 65: [2, 35], 68: [2, 35], 72: [2, 35], 75: [2, 35], 80: [2, 35], 81: [2, 35], 82: [2, 35], 83: [2, 35], 84: [2, 35], 85: [2, 35] }, { 23: [2, 36], 33: [2, 36], 54: [2, 36], 65: [2, 36], 68: [2, 36], 72: [2, 36], 75: [2, 36], 80: [2, 36], 81: [2, 36], 82: [2, 36], 83: [2, 36], 84: [2, 36], 85: [2, 36] }, { 23: [2, 37], 33: [2, 37], 54: [2, 37], 65: [2, 37], 68: [2, 37], 72: [2, 37], 75: [2, 37], 80: [2, 37], 81: [2, 37], 82: [2, 37], 83: [2, 37], 84: [2, 37], 85: [2, 37] }, { 23: [2, 38], 33: [2, 38], 54: [2, 38], 65: [2, 38], 68: [2, 38], 72: [2, 38], 75: [2, 38], 80: [2, 38], 81: [2, 38], 82: [2, 38], 83: [2, 38], 84: [2, 38], 85: [2, 38] }, { 23: [2, 39], 33: [2, 39], 54: [2, 39], 65: [2, 39], 68: [2, 39], 72: [2, 39], 75: [2, 39], 80: [2, 39], 81: [2, 39], 82: [2, 39], 83: [2, 39], 84: [2, 39], 85: [2, 39] }, { 23: [2, 43], 33: [2, 43], 54: [2, 43], 65: [2, 43], 68: [2, 43], 72: [2, 43], 75: [2, 43], 80: [2, 43], 81: [2, 43], 82: [2, 43], 83: [2, 43], 84: [2, 43], 85: [2, 43], 87: [1, 51] }, { 72: [1, 35], 86: 52 }, { 23: [2, 45], 33: [2, 45], 54: [2, 45], 65: [2, 45], 68: [2, 45], 72: [2, 45], 75: [2, 45], 80: [2, 45], 81: [2, 45], 82: [2, 45], 83: [2, 45], 84: [2, 45], 85: [2, 45], 87: [2, 45] }, { 52: 53, 54: [2, 82], 65: [2, 82], 72: [2, 82], 80: [2, 82], 81: [2, 82], 82: [2, 82], 83: [2, 82], 84: [2, 82], 85: [2, 82] }, { 25: 54, 38: 56, 39: [1, 58], 43: 57, 44: [1, 59], 45: 55, 47: [2, 54] }, { 28: 60, 43: 61, 44: [1, 59], 47: [2, 56] }, { 13: 63, 15: [1, 20], 18: [1, 62] }, { 15: [2, 48], 18: [2, 48] }, { 33: [2, 86], 57: 64, 65: [2, 86], 72: [2, 86], 80: [2, 86], 81: [2, 86], 82: [2, 86], 83: [2, 86], 84: [2, 86], 85: [2, 86] }, { 33: [2, 40], 65: [2, 40], 72: [2, 40], 80: [2, 40], 81: [2, 40], 82: [2, 40], 83: [2, 40], 84: [2, 40], 85: [2, 40] }, { 33: [2, 41], 65: [2, 41], 72: [2, 41], 80: [2, 41], 81: [2, 41], 82: [2, 41], 83: [2, 41], 84: [2, 41], 85: [2, 41] }, { 20: 65, 72: [1, 35], 78: 26, 79: 27, 80: [1, 28], 81: [1, 29], 82: [1, 30], 83: [1, 31], 84: [1, 32], 85: [1, 34], 86: 33 }, { 26: 66, 47: [1, 67] }, { 30: 68, 33: [2, 58], 65: [2, 58], 72: [2, 58], 75: [2, 58], 80: [2, 58], 81: [2, 58], 82: [2, 58], 83: [2, 58], 84: [2, 58], 85: [2, 58] }, { 33: [2, 64], 35: 69, 65: [2, 64], 72: [2, 64], 75: [2, 64], 80: [2, 64], 81: [2, 64], 82: [2, 64], 83: [2, 64], 84: [2, 64], 85: [2, 64] }, { 21: 70, 23: [2, 50], 65: [2, 50], 72: [2, 50], 80: [2, 50], 81: [2, 50], 82: [2, 50], 83: [2, 50], 84: [2, 50], 85: [2, 50] }, { 33: [2, 90], 61: 71, 65: [2, 90], 72: [2, 90], 80: [2, 90], 81: [2, 90], 82: [2, 90], 83: [2, 90], 84: [2, 90], 85: [2, 90] }, { 20: 75, 33: [2, 80], 50: 72, 63: 73, 64: 76, 65: [1, 44], 69: 74, 70: 77, 71: 78, 72: [1, 79], 78: 26, 79: 27, 80: [1, 28], 81: [1, 29], 82: [1, 30], 83: [1, 31], 84: [1, 32], 85: [1, 34], 86: 33 }, { 72: [1, 80] }, { 23: [2, 42], 33: [2, 42], 54: [2, 42], 65: [2, 42], 68: [2, 42], 72: [2, 42], 75: [2, 42], 80: [2, 42], 81: [2, 42], 82: [2, 42], 83: [2, 42], 84: [2, 42], 85: [2, 42], 87: [1, 51] }, { 20: 75, 53: 81, 54: [2, 84], 63: 82, 64: 76, 65: [1, 44], 69: 83, 70: 77, 71: 78, 72: [1, 79], 78: 26, 79: 27, 80: [1, 28], 81: [1, 29], 82: [1, 30], 83: [1, 31], 84: [1, 32], 85: [1, 34], 86: 33 }, { 26: 84, 47: [1, 67] }, { 47: [2, 55] }, { 4: 85, 6: 3, 14: [2, 46], 15: [2, 46], 19: [2, 46], 29: [2, 46], 34: [2, 46], 39: [2, 46], 44: [2, 46], 47: [2, 46], 48: [2, 46], 51: [2, 46], 55: [2, 46], 60: [2, 46] }, { 47: [2, 20] }, { 20: 86, 72: [1, 35], 78: 26, 79: 27, 80: [1, 28], 81: [1, 29], 82: [1, 30], 83: [1, 31], 84: [1, 32], 85: [1, 34], 86: 33 }, { 4: 87, 6: 3, 14: [2, 46], 15: [2, 46], 19: [2, 46], 29: [2, 46], 34: [2, 46], 47: [2, 46], 48: [2, 46], 51: [2, 46], 55: [2, 46], 60: [2, 46] }, { 26: 88, 47: [1, 67] }, { 47: [2, 57] }, { 5: [2, 11], 14: [2, 11], 15: [2, 11], 19: [2, 11], 29: [2, 11], 34: [2, 11], 39: [2, 11], 44: [2, 11], 47: [2, 11], 48: [2, 11], 51: [2, 11], 55: [2, 11], 60: [2, 11] }, { 15: [2, 49], 18: [2, 49] }, { 20: 75, 33: [2, 88], 58: 89, 63: 90, 64: 76, 65: [1, 44], 69: 91, 70: 77, 71: 78, 72: [1, 79], 78: 26, 79: 27, 80: [1, 28], 81: [1, 29], 82: [1, 30], 83: [1, 31], 84: [1, 32], 85: [1, 34], 86: 33 }, { 65: [2, 94], 66: 92, 68: [2, 94], 72: [2, 94], 80: [2, 94], 81: [2, 94], 82: [2, 94], 83: [2, 94], 84: [2, 94], 85: [2, 94] }, { 5: [2, 25], 14: [2, 25], 15: [2, 25], 19: [2, 25], 29: [2, 25], 34: [2, 25], 39: [2, 25], 44: [2, 25], 47: [2, 25], 48: [2, 25], 51: [2, 25], 55: [2, 25], 60: [2, 25] }, { 20: 93, 72: [1, 35], 78: 26, 79: 27, 80: [1, 28], 81: [1, 29], 82: [1, 30], 83: [1, 31], 84: [1, 32], 85: [1, 34], 86: 33 }, { 20: 75, 31: 94, 33: [2, 60], 63: 95, 64: 76, 65: [1, 44], 69: 96, 70: 77, 71: 78, 72: [1, 79], 75: [2, 60], 78: 26, 79: 27, 80: [1, 28], 81: [1, 29], 82: [1, 30], 83: [1, 31], 84: [1, 32], 85: [1, 34], 86: 33 }, { 20: 75, 33: [2, 66], 36: 97, 63: 98, 64: 76, 65: [1, 44], 69: 99, 70: 77, 71: 78, 72: [1, 79], 75: [2, 66], 78: 26, 79: 27, 80: [1, 28], 81: [1, 29], 82: [1, 30], 83: [1, 31], 84: [1, 32], 85: [1, 34], 86: 33 }, { 20: 75, 22: 100, 23: [2, 52], 63: 101, 64: 76, 65: [1, 44], 69: 102, 70: 77, 71: 78, 72: [1, 79], 78: 26, 79: 27, 80: [1, 28], 81: [1, 29], 82: [1, 30], 83: [1, 31], 84: [1, 32], 85: [1, 34], 86: 33 }, { 20: 75, 33: [2, 92], 62: 103, 63: 104, 64: 76, 65: [1, 44], 69: 105, 70: 77, 71: 78, 72: [1, 79], 78: 26, 79: 27, 80: [1, 28], 81: [1, 29], 82: [1, 30], 83: [1, 31], 84: [1, 32], 85: [1, 34], 86: 33 }, { 33: [1, 106] }, { 33: [2, 79], 65: [2, 79], 72: [2, 79], 80: [2, 79], 81: [2, 79], 82: [2, 79], 83: [2, 79], 84: [2, 79], 85: [2, 79] }, { 33: [2, 81] }, { 23: [2, 27], 33: [2, 27], 54: [2, 27], 65: [2, 27], 68: [2, 27], 72: [2, 27], 75: [2, 27], 80: [2, 27], 81: [2, 27], 82: [2, 27], 83: [2, 27], 84: [2, 27], 85: [2, 27] }, { 23: [2, 28], 33: [2, 28], 54: [2, 28], 65: [2, 28], 68: [2, 28], 72: [2, 28], 75: [2, 28], 80: [2, 28], 81: [2, 28], 82: [2, 28], 83: [2, 28], 84: [2, 28], 85: [2, 28] }, { 23: [2, 30], 33: [2, 30], 54: [2, 30], 68: [2, 30], 71: 107, 72: [1, 108], 75: [2, 30] }, { 23: [2, 98], 33: [2, 98], 54: [2, 98], 68: [2, 98], 72: [2, 98], 75: [2, 98] }, { 23: [2, 45], 33: [2, 45], 54: [2, 45], 65: [2, 45], 68: [2, 45], 72: [2, 45], 73: [1, 109], 75: [2, 45], 80: [2, 45], 81: [2, 45], 82: [2, 45], 83: [2, 45], 84: [2, 45], 85: [2, 45], 87: [2, 45] }, { 23: [2, 44], 33: [2, 44], 54: [2, 44], 65: [2, 44], 68: [2, 44], 72: [2, 44], 75: [2, 44], 80: [2, 44], 81: [2, 44], 82: [2, 44], 83: [2, 44], 84: [2, 44], 85: [2, 44], 87: [2, 44] }, { 54: [1, 110] }, { 54: [2, 83], 65: [2, 83], 72: [2, 83], 80: [2, 83], 81: [2, 83], 82: [2, 83], 83: [2, 83], 84: [2, 83], 85: [2, 83] }, { 54: [2, 85] }, { 5: [2, 13], 14: [2, 13], 15: [2, 13], 19: [2, 13], 29: [2, 13], 34: [2, 13], 39: [2, 13], 44: [2, 13], 47: [2, 13], 48: [2, 13], 51: [2, 13], 55: [2, 13], 60: [2, 13] }, { 38: 56, 39: [1, 58], 43: 57, 44: [1, 59], 45: 112, 46: 111, 47: [2, 76] }, { 33: [2, 70], 40: 113, 65: [2, 70], 72: [2, 70], 75: [2, 70], 80: [2, 70], 81: [2, 70], 82: [2, 70], 83: [2, 70], 84: [2, 70], 85: [2, 70] }, { 47: [2, 18] }, { 5: [2, 14], 14: [2, 14], 15: [2, 14], 19: [2, 14], 29: [2, 14], 34: [2, 14], 39: [2, 14], 44: [2, 14], 47: [2, 14], 48: [2, 14], 51: [2, 14], 55: [2, 14], 60: [2, 14] }, { 33: [1, 114] }, { 33: [2, 87], 65: [2, 87], 72: [2, 87], 80: [2, 87], 81: [2, 87], 82: [2, 87], 83: [2, 87], 84: [2, 87], 85: [2, 87] }, { 33: [2, 89] }, { 20: 75, 63: 116, 64: 76, 65: [1, 44], 67: 115, 68: [2, 96], 69: 117, 70: 77, 71: 78, 72: [1, 79], 78: 26, 79: 27, 80: [1, 28], 81: [1, 29], 82: [1, 30], 83: [1, 31], 84: [1, 32], 85: [1, 34], 86: 33 }, { 33: [1, 118] }, { 32: 119, 33: [2, 62], 74: 120, 75: [1, 121] }, { 33: [2, 59], 65: [2, 59], 72: [2, 59], 75: [2, 59], 80: [2, 59], 81: [2, 59], 82: [2, 59], 83: [2, 59], 84: [2, 59], 85: [2, 59] }, { 33: [2, 61], 75: [2, 61] }, { 33: [2, 68], 37: 122, 74: 123, 75: [1, 121] }, { 33: [2, 65], 65: [2, 65], 72: [2, 65], 75: [2, 65], 80: [2, 65], 81: [2, 65], 82: [2, 65], 83: [2, 65], 84: [2, 65], 85: [2, 65] }, { 33: [2, 67], 75: [2, 67] }, { 23: [1, 124] }, { 23: [2, 51], 65: [2, 51], 72: [2, 51], 80: [2, 51], 81: [2, 51], 82: [2, 51], 83: [2, 51], 84: [2, 51], 85: [2, 51] }, { 23: [2, 53] }, { 33: [1, 125] }, { 33: [2, 91], 65: [2, 91], 72: [2, 91], 80: [2, 91], 81: [2, 91], 82: [2, 91], 83: [2, 91], 84: [2, 91], 85: [2, 91] }, { 33: [2, 93] }, { 5: [2, 22], 14: [2, 22], 15: [2, 22], 19: [2, 22], 29: [2, 22], 34: [2, 22], 39: [2, 22], 44: [2, 22], 47: [2, 22], 48: [2, 22], 51: [2, 22], 55: [2, 22], 60: [2, 22] }, { 23: [2, 99], 33: [2, 99], 54: [2, 99], 68: [2, 99], 72: [2, 99], 75: [2, 99] }, { 73: [1, 109] }, { 20: 75, 63: 126, 64: 76, 65: [1, 44], 72: [1, 35], 78: 26, 79: 27, 80: [1, 28], 81: [1, 29], 82: [1, 30], 83: [1, 31], 84: [1, 32], 85: [1, 34], 86: 33 }, { 5: [2, 23], 14: [2, 23], 15: [2, 23], 19: [2, 23], 29: [2, 23], 34: [2, 23], 39: [2, 23], 44: [2, 23], 47: [2, 23], 48: [2, 23], 51: [2, 23], 55: [2, 23], 60: [2, 23] }, { 47: [2, 19] }, { 47: [2, 77] }, { 20: 75, 33: [2, 72], 41: 127, 63: 128, 64: 76, 65: [1, 44], 69: 129, 70: 77, 71: 78, 72: [1, 79], 75: [2, 72], 78: 26, 79: 27, 80: [1, 28], 81: [1, 29], 82: [1, 30], 83: [1, 31], 84: [1, 32], 85: [1, 34], 86: 33 }, { 5: [2, 24], 14: [2, 24], 15: [2, 24], 19: [2, 24], 29: [2, 24], 34: [2, 24], 39: [2, 24], 44: [2, 24], 47: [2, 24], 48: [2, 24], 51: [2, 24], 55: [2, 24], 60: [2, 24] }, { 68: [1, 130] }, { 65: [2, 95], 68: [2, 95], 72: [2, 95], 80: [2, 95], 81: [2, 95], 82: [2, 95], 83: [2, 95], 84: [2, 95], 85: [2, 95] }, { 68: [2, 97] }, { 5: [2, 21], 14: [2, 21], 15: [2, 21], 19: [2, 21], 29: [2, 21], 34: [2, 21], 39: [2, 21], 44: [2, 21], 47: [2, 21], 48: [2, 21], 51: [2, 21], 55: [2, 21], 60: [2, 21] }, { 33: [1, 131] }, { 33: [2, 63] }, { 72: [1, 133], 76: 132 }, { 33: [1, 134] }, { 33: [2, 69] }, { 15: [2, 12] }, { 14: [2, 26], 15: [2, 26], 19: [2, 26], 29: [2, 26], 34: [2, 26], 47: [2, 26], 48: [2, 26], 51: [2, 26], 55: [2, 26], 60: [2, 26] }, { 23: [2, 31], 33: [2, 31], 54: [2, 31], 68: [2, 31], 72: [2, 31], 75: [2, 31] }, { 33: [2, 74], 42: 135, 74: 136, 75: [1, 121] }, { 33: [2, 71], 65: [2, 71], 72: [2, 71], 75: [2, 71], 80: [2, 71], 81: [2, 71], 82: [2, 71], 83: [2, 71], 84: [2, 71], 85: [2, 71] }, { 33: [2, 73], 75: [2, 73] }, { 23: [2, 29], 33: [2, 29], 54: [2, 29], 65: [2, 29], 68: [2, 29], 72: [2, 29], 75: [2, 29], 80: [2, 29], 81: [2, 29], 82: [2, 29], 83: [2, 29], 84: [2, 29], 85: [2, 29] }, { 14: [2, 15], 15: [2, 15], 19: [2, 15], 29: [2, 15], 34: [2, 15], 39: [2, 15], 44: [2, 15], 47: [2, 15], 48: [2, 15], 51: [2, 15], 55: [2, 15], 60: [2, 15] }, { 72: [1, 138], 77: [1, 137] }, { 72: [2, 100], 77: [2, 100] }, { 14: [2, 16], 15: [2, 16], 19: [2, 16], 29: [2, 16], 34: [2, 16], 44: [2, 16], 47: [2, 16], 48: [2, 16], 51: [2, 16], 55: [2, 16], 60: [2, 16] }, { 33: [1, 139] }, { 33: [2, 75] }, { 33: [2, 32] }, { 72: [2, 101], 77: [2, 101] }, { 14: [2, 17], 15: [2, 17], 19: [2, 17], 29: [2, 17], 34: [2, 17], 39: [2, 17], 44: [2, 17], 47: [2, 17], 48: [2, 17], 51: [2, 17], 55: [2, 17], 60: [2, 17] }], defaultActions: { 4: [2, 1], 55: [2, 55], 57: [2, 20], 61: [2, 57], 74: [2, 81], 83: [2, 85], 87: [2, 18], 91: [2, 89], 102: [2, 53], 105: [2, 93], 111: [2, 19], 112: [2, 77], 117: [2, 97], 120: [2, 63], 123: [2, 69], 124: [2, 12], 136: [2, 75], 137: [2, 32] }, parseError: function (a, b) { throw new Error(a) }, parse: function (a) { function b() { var a; return a = c.lexer.lex() || 1, "number" != typeof a && (a = c.symbols_[a] || a), a } var c = this, d = [0], e = [null], f = [], g = this.table, h = "", i = 0, j = 0, k = 0; this.lexer.setInput(a), this.lexer.yy = this.yy, this.yy.lexer = this.lexer, this.yy.parser = this, "undefined" == typeof this.lexer.yylloc && (this.lexer.yylloc = {}); var l = this.lexer.yylloc; f.push(l); var m = this.lexer.options && this.lexer.options.ranges; "function" == typeof this.yy.parseError && (this.parseError = this.yy.parseError); for (var n, o, p, q, r, s, t, u, v, w = {}; ;) { if (p = d[d.length - 1], this.defaultActions[p] ? q = this.defaultActions[p] : ((null === n || "undefined" == typeof n) && (n = b()), q = g[p] && g[p][n]), "undefined" == typeof q || !q.length || !q[0]) { var x = ""; if (!k) { v = []; for (s in g[p]) this.terminals_[s] && s > 2 && v.push("'" + this.terminals_[s] + "'"); x = this.lexer.showPosition ? "Parse error on line " + (i + 1) + ":\n" + this.lexer.showPosition() + "\nExpecting " + v.join(", ") + ", got '" + (this.terminals_[n] || n) + "'" : "Parse error on line " + (i + 1) + ": Unexpected " + (1 == n ? "end of input" : "'" + (this.terminals_[n] || n) + "'"), this.parseError(x, { text: this.lexer.match, token: this.terminals_[n] || n, line: this.lexer.yylineno, loc: l, expected: v }) } } if (q[0] instanceof Array && q.length > 1) throw new Error("Parse Error: multiple actions possible at state: " + p + ", token: " + n); switch (q[0]) { case 1: d.push(n), e.push(this.lexer.yytext), f.push(this.lexer.yylloc), d.push(q[1]), n = null, o ? (n = o, o = null) : (j = this.lexer.yyleng, h = this.lexer.yytext, i = this.lexer.yylineno, l = this.lexer.yylloc, k > 0 && k--); break; case 2: if (t = this.productions_[q[1]][1], w.$ = e[e.length - t], w._$ = { first_line: f[f.length - (t || 1)].first_line, last_line: f[f.length - 1].last_line, first_column: f[f.length - (t || 1)].first_column, last_column: f[f.length - 1].last_column }, m && (w._$.range = [f[f.length - (t || 1)].range[0], f[f.length - 1].range[1]]), r = this.performAction.call(w, h, j, i, this.yy, q[1], e, f), "undefined" != typeof r) return r; t && (d = d.slice(0, -1 * t * 2), e = e.slice(0, -1 * t), f = f.slice(0, -1 * t)), d.push(this.productions_[q[1]][0]), e.push(w.$), f.push(w._$), u = g[d[d.length - 2]][d[d.length - 1]], d.push(u); break; case 3: return !0 } } return !0 } }, c = function () { var a = { EOF: 1, parseError: function (a, b) { if (!this.yy.parser) throw new Error(a); this.yy.parser.parseError(a, b) }, setInput: function (a) { return this._input = a, this._more = this._less = this.done = !1, this.yylineno = this.yyleng = 0, this.yytext = this.matched = this.match = "", this.conditionStack = ["INITIAL"], this.yylloc = { first_line: 1, first_column: 0, last_line: 1, last_column: 0 }, this.options.ranges && (this.yylloc.range = [0, 0]), this.offset = 0, this }, input: function () { var a = this._input[0]; this.yytext += a, this.yyleng++, this.offset++, this.match += a, this.matched += a; var b = a.match(/(?:\r\n?|\n).*/g); return b ? (this.yylineno++, this.yylloc.last_line++) : this.yylloc.last_column++, this.options.ranges && this.yylloc.range[1]++, this._input = this._input.slice(1), a }, unput: function (a) { var b = a.length, c = a.split(/(?:\r\n?|\n)/g); this._input = a + this._input, this.yytext = this.yytext.substr(0, this.yytext.length - b - 1), this.offset -= b; var d = this.match.split(/(?:\r\n?|\n)/g); this.match = this.match.substr(0, this.match.length - 1), this.matched = this.matched.substr(0, this.matched.length - 1), c.length - 1 && (this.yylineno -= c.length - 1); var e = this.yylloc.range; return this.yylloc = { first_line: this.yylloc.first_line, last_line: this.yylineno + 1, first_column: this.yylloc.first_column, last_column: c ? (c.length === d.length ? this.yylloc.first_column : 0) + d[d.length - c.length].length - c[0].length : this.yylloc.first_column - b }, this.options.ranges && (this.yylloc.range = [e[0], e[0] + this.yyleng - b]), this }, more: function () { return this._more = !0, this }, less: function (a) { this.unput(this.match.slice(a)) }, pastInput: function () { var a = this.matched.substr(0, this.matched.length - this.match.length); return (a.length > 20 ? "..." : "") + a.substr(-20).replace(/\n/g, "") }, upcomingInput: function () { var a = this.match; return a.length < 20 && (a += this._input.substr(0, 20 - a.length)), (a.substr(0, 20) + (a.length > 20 ? "..." : "")).replace(/\n/g, "") }, showPosition: function () { var a = this.pastInput(), b = new Array(a.length + 1).join("-"); return a + this.upcomingInput() + "\n" + b + "^" }, next: function () { if (this.done) return this.EOF; this._input || (this.done = !0); var a, b, c, d, e; this._more || (this.yytext = "", this.match = ""); for (var f = this._currentRules(), g = 0; g < f.length && (c = this._input.match(this.rules[f[g]]), !c || b && !(c[0].length > b[0].length) || (b = c, d = g, this.options.flex)) ; g++); return b ? (e = b[0].match(/(?:\r\n?|\n).*/g), e && (this.yylineno += e.length), this.yylloc = { first_line: this.yylloc.last_line, last_line: this.yylineno + 1, first_column: this.yylloc.last_column, last_column: e ? e[e.length - 1].length - e[e.length - 1].match(/\r?\n?/)[0].length : this.yylloc.last_column + b[0].length }, this.yytext += b[0], this.match += b[0], this.matches = b, this.yyleng = this.yytext.length, this.options.ranges && (this.yylloc.range = [this.offset, this.offset += this.yyleng]), this._more = !1, this._input = this._input.slice(b[0].length), this.matched += b[0], a = this.performAction.call(this, this.yy, this, f[d], this.conditionStack[this.conditionStack.length - 1]), this.done && this._input && (this.done = !1), a ? a : void 0) : "" === this._input ? this.EOF : this.parseError("Lexical error on line " + (this.yylineno + 1) + ". Unrecognized text.\n" + this.showPosition(), { text: "", token: null, line: this.yylineno }) }, lex: function () { var a = this.next(); return "undefined" != typeof a ? a : this.lex() }, begin: function (a) { this.conditionStack.push(a) }, popState: function () { return this.conditionStack.pop() }, _currentRules: function () { return this.conditions[this.conditionStack[this.conditionStack.length - 1]].rules }, topState: function () { return this.conditionStack[this.conditionStack.length - 2] }, pushState: function (a) { this.begin(a) } }; return a.options = {}, a.performAction = function (a, b, c, d) { function e(a, c) { return b.yytext = b.yytext.substr(a, b.yyleng - c) } switch (c) { case 0: if ("\\\\" === b.yytext.slice(-2) ? (e(0, 1), this.begin("mu")) : "\\" === b.yytext.slice(-1) ? (e(0, 1), this.begin("emu")) : this.begin("mu"), b.yytext) return 15; break; case 1: return 15; case 2: return this.popState(), 15; case 3: return this.begin("raw"), 15; case 4: return this.popState(), "raw" === this.conditionStack[this.conditionStack.length - 1] ? 15 : (b.yytext = b.yytext.substr(5, b.yyleng - 9), "END_RAW_BLOCK"); case 5: return 15; case 6: return this.popState(), 14; case 7: return 65; case 8: return 68; case 9: return 19; case 10: return this.popState(), this.begin("raw"), 23; case 11: return 55; case 12: return 60; case 13: return 29; case 14: return 47; case 15: return this.popState(), 44; case 16: return this.popState(), 44; case 17: return 34; case 18: return 39; case 19: return 51; case 20: return 48; case 21: this.unput(b.yytext), this.popState(), this.begin("com"); break; case 22: return this.popState(), 14; case 23: return 48; case 24: return 73; case 25: return 72; case 26: return 72; case 27: return 87; case 28: break; case 29: return this.popState(), 54; case 30: return this.popState(), 33; case 31: return b.yytext = e(1, 2).replace(/\\"/g, '"'), 80; case 32: return b.yytext = e(1, 2).replace(/\\'/g, "'"), 80; case 33: return 85; case 34: return 82; case 35: return 82; case 36: return 83; case 37: return 84; case 38: return 81; case 39: return 75; case 40: return 77; case 41: return 72; case 42: return b.yytext = b.yytext.replace(/\\([\\\]])/g, "$1"), 72; case 43: return "INVALID"; case 44: return 5 } }, a.rules = [/^(?:[^\x00]*?(?=(\{\{)))/, /^(?:[^\x00]+)/, /^(?:[^\x00]{2,}?(?=(\{\{|\\\{\{|\\\\\{\{|$)))/, /^(?:\{\{\{\{(?=[^\/]))/, /^(?:\{\{\{\{\/[^\s!"#%-,\.\/;->@\[-\^`\{-~]+(?=[=}\s\/.])\}\}\}\})/, /^(?:[^\x00]*?(?=(\{\{\{\{)))/, /^(?:[\s\S]*?--(~)?\}\})/, /^(?:\()/, /^(?:\))/, /^(?:\{\{\{\{)/, /^(?:\}\}\}\})/, /^(?:\{\{(~)?>)/, /^(?:\{\{(~)?#>)/, /^(?:\{\{(~)?#\*?)/, /^(?:\{\{(~)?\/)/, /^(?:\{\{(~)?\^\s*(~)?\}\})/, /^(?:\{\{(~)?\s*else\s*(~)?\}\})/, /^(?:\{\{(~)?\^)/, /^(?:\{\{(~)?\s*else\b)/, /^(?:\{\{(~)?\{)/, /^(?:\{\{(~)?&)/, /^(?:\{\{(~)?!--)/, /^(?:\{\{(~)?![\s\S]*?\}\})/, /^(?:\{\{(~)?\*?)/, /^(?:=)/, /^(?:\.\.)/, /^(?:\.(?=([=~}\s\/.)|])))/, /^(?:[\/.])/, /^(?:\s+)/, /^(?:\}(~)?\}\})/, /^(?:(~)?\}\})/, /^(?:"(\\["]|[^"])*")/, /^(?:'(\\[']|[^'])*')/, /^(?:@)/, /^(?:true(?=([~}\s)])))/, /^(?:false(?=([~}\s)])))/, /^(?:undefined(?=([~}\s)])))/, /^(?:null(?=([~}\s)])))/, /^(?:-?[0-9]+(?:\.[0-9]+)?(?=([~}\s)])))/, /^(?:as\s+\|)/, /^(?:\|)/, /^(?:([^\s!"#%-,\.\/;->@\[-\^`\{-~]+(?=([=~}\s\/.)|]))))/, /^(?:\[(\\\]|[^\]])*\])/, /^(?:.)/, /^(?:$)/], a.conditions = { mu: { rules: [7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44], inclusive: !1 }, emu: { rules: [2], inclusive: !1 }, com: { rules: [6], inclusive: !1 }, raw: { rules: [3, 4, 5], inclusive: !1 }, INITIAL: { rules: [0, 1, 44], inclusive: !0 } }, a }(); return b.lexer = c, a.prototype = b, b.Parser = a, new a }(); b.__esModule = !0, b["default"] = d }, function (a, b, c) { "use strict"; function d() { var a = arguments.length <= 0 || void 0 === arguments[0] ? {} : arguments[0]; this.options = a } function e(a, b, c) { void 0 === b && (b = a.length); var d = a[b - 1], e = a[b - 2]; return d ? "ContentStatement" === d.type ? (e || !c ? /\r?\n\s*?$/ : /(^|\r?\n)\s*?$/).test(d.original) : void 0 : c } function f(a, b, c) { void 0 === b && (b = -1); var d = a[b + 1], e = a[b + 2]; return d ? "ContentStatement" === d.type ? (e || !c ? /^\s*?\r?\n/ : /^\s*?(\r?\n|$)/).test(d.original) : void 0 : c } function g(a, b, c) { var d = a[null == b ? 0 : b + 1]; if (d && "ContentStatement" === d.type && (c || !d.rightStripped)) { var e = d.value; d.value = d.value.replace(c ? /^\s+/ : /^[ \t]*\r?\n?/, ""), d.rightStripped = d.value !== e } } function h(a, b, c) { var d = a[null == b ? a.length - 1 : b - 1]; if (d && "ContentStatement" === d.type && (c || !d.leftStripped)) { var e = d.value; return d.value = d.value.replace(c ? /\s+$/ : /[ \t]+$/, ""), d.leftStripped = d.value !== e, d.leftStripped } } var i = c(8)["default"]; b.__esModule = !0; var j = c(6), k = i(j); d.prototype = new k["default"], d.prototype.Program = function (a) { var b = !this.options.ignoreStandalone, c = !this.isRootSeen; this.isRootSeen = !0; for (var d = a.body, i = 0, j = d.length; j > i; i++) { var k = d[i], l = this.accept(k); if (l) { var m = e(d, i, c), n = f(d, i, c), o = l.openStandalone && m, p = l.closeStandalone && n, q = l.inlineStandalone && m && n; l.close && g(d, i, !0), l.open && h(d, i, !0), b && q && (g(d, i), h(d, i) && "PartialStatement" === k.type && (k.indent = /([ \t]+$)/.exec(d[i - 1].original)[1])), b && o && (g((k.program || k.inverse).body), h(d, i)), b && p && (g(d, i), h((k.inverse || k.program).body)) } } return a }, d.prototype.BlockStatement = d.prototype.DecoratorBlock = d.prototype.PartialBlockStatement = function (a) { this.accept(a.program), this.accept(a.inverse); var b = a.program || a.inverse, c = a.program && a.inverse, d = c, i = c; if (c && c.chained) for (d = c.body[0].program; i.chained;) i = i.body[i.body.length - 1].program; var j = { open: a.openStrip.open, close: a.closeStrip.close, openStandalone: f(b.body), closeStandalone: e((d || b).body) }; if (a.openStrip.close && g(b.body, null, !0), c) { var k = a.inverseStrip; k.open && h(b.body, null, !0), k.close && g(d.body, null, !0), a.closeStrip.open && h(i.body, null, !0), !this.options.ignoreStandalone && e(b.body) && f(d.body) && (h(b.body), g(d.body)) } else a.closeStrip.open && h(b.body, null, !0); return j }, d.prototype.Decorator = d.prototype.MustacheStatement = function (a) { return a.strip }, d.prototype.PartialStatement = d.prototype.CommentStatement = function (a) { var b = a.strip || {}; return { inlineStandalone: !0, open: b.open, close: b.close } }, b["default"] = d, a.exports = b["default"] }, function (a, b, c) {
        "use strict"; function d(a, b) { if (b = b.path ? b.path.original : b, a.path.original !== b) { var c = { loc: a.path.loc }; throw new q["default"](a.path.original + " doesn't match " + b, c) } } function e(a, b) { this.source = a, this.start = { line: b.first_line, column: b.first_column }, this.end = { line: b.last_line, column: b.last_column } } function f(a) { return /^\[.*\]$/.test(a) ? a.substr(1, a.length - 2) : a } function g(a, b) { return { open: "~" === a.charAt(2), close: "~" === b.charAt(b.length - 3) } } function h(a) { return a.replace(/^\{\{~?\!-?-?/, "").replace(/-?-?~?\}\}$/, "") } function i(a, b, c) { c = this.locInfo(c); for (var d = a ? "@" : "", e = [], f = 0, g = "", h = 0, i = b.length; i > h; h++) { var j = b[h].part, k = b[h].original !== j; if (d += (b[h].separator || "") + j, k || ".." !== j && "." !== j && "this" !== j) e.push(j); else { if (e.length > 0) throw new q["default"]("Invalid path: " + d, { loc: c }); ".." === j && (f++, g += "../") } } return { type: "PathExpression", data: a, depth: f, parts: e, original: d, loc: c } } function j(a, b, c, d, e, f) {
            var g = d.charAt(3) || d.charAt(2), h = "{" !== g && "&" !== g, i = /\*/.test(d); return {
                type: i ? "Decorator" : "MustacheStatement", path: a, params: b, hash: c, escaped: h,
                strip: e, loc: this.locInfo(f)
            }
        } function k(a, b, c, e) { d(a, c), e = this.locInfo(e); var f = { type: "Program", body: b, strip: {}, loc: e }; return { type: "BlockStatement", path: a.path, params: a.params, hash: a.hash, program: f, openStrip: {}, inverseStrip: {}, closeStrip: {}, loc: e } } function l(a, b, c, e, f, g) { e && e.path && d(a, e); var h = /\*/.test(a.open); b.blockParams = a.blockParams; var i = void 0, j = void 0; if (c) { if (h) throw new q["default"]("Unexpected inverse block on decorator", c); c.chain && (c.program.body[0].closeStrip = e.strip), j = c.strip, i = c.program } return f && (f = i, i = b, b = f), { type: h ? "DecoratorBlock" : "BlockStatement", path: a.path, params: a.params, hash: a.hash, program: b, inverse: i, openStrip: a.strip, inverseStrip: j, closeStrip: e && e.strip, loc: this.locInfo(g) } } function m(a, b) { if (!b && a.length) { var c = a[0].loc, d = a[a.length - 1].loc; c && d && (b = { source: c.source, start: { line: c.start.line, column: c.start.column }, end: { line: d.end.line, column: d.end.column } }) } return { type: "Program", body: a, strip: {}, loc: b } } function n(a, b, c, e) { return d(a, c), { type: "PartialBlockStatement", name: a.path, params: a.params, hash: a.hash, program: b, openStrip: a.strip, closeStrip: c && c.strip, loc: this.locInfo(e) } } var o = c(8)["default"]; b.__esModule = !0, b.SourceLocation = e, b.id = f, b.stripFlags = g, b.stripComment = h, b.preparePath = i, b.prepareMustache = j, b.prepareRawBlock = k, b.prepareBlock = l, b.prepareProgram = m, b.preparePartialBlock = n; var p = c(12), q = o(p)
    }, function (a, b, c) { "use strict"; function d(a, b, c) { if (f.isArray(a)) { for (var d = [], e = 0, g = a.length; g > e; e++) d.push(b.wrap(a[e], c)); return d } return "boolean" == typeof a || "number" == typeof a ? a + "" : a } function e(a) { this.srcFile = a, this.source = [] } b.__esModule = !0; var f = c(13), g = void 0; try { } catch (h) { } g || (g = function (a, b, c, d) { this.src = "", d && this.add(d) }, g.prototype = { add: function (a) { f.isArray(a) && (a = a.join("")), this.src += a }, prepend: function (a) { f.isArray(a) && (a = a.join("")), this.src = a + this.src }, toStringWithSourceMap: function () { return { code: this.toString() } }, toString: function () { return this.src } }), e.prototype = { isEmpty: function () { return !this.source.length }, prepend: function (a, b) { this.source.unshift(this.wrap(a, b)) }, push: function (a, b) { this.source.push(this.wrap(a, b)) }, merge: function () { var a = this.empty(); return this.each(function (b) { a.add(["  ", b, "\n"]) }), a }, each: function (a) { for (var b = 0, c = this.source.length; c > b; b++) a(this.source[b]) }, empty: function () { var a = this.currentLocation || { start: {} }; return new g(a.start.line, a.start.column, this.srcFile) }, wrap: function (a) { var b = arguments.length <= 1 || void 0 === arguments[1] ? this.currentLocation || { start: {} } : arguments[1]; return a instanceof g ? a : (a = d(a, this, b), new g(b.start.line, b.start.column, this.srcFile, a)) }, functionCall: function (a, b, c) { return c = this.generateList(c), this.wrap([a, b ? "." + b + "(" : "(", c, ")"]) }, quotedString: function (a) { return '"' + (a + "").replace(/\\/g, "\\\\").replace(/"/g, '\\"').replace(/\n/g, "\\n").replace(/\r/g, "\\r").replace(/\u2028/g, "\\u2028").replace(/\u2029/g, "\\u2029") + '"' }, objectLiteral: function (a) { var b = []; for (var c in a) if (a.hasOwnProperty(c)) { var e = d(a[c], this); "undefined" !== e && b.push([this.quotedString(c), ":", e]) } var f = this.generateList(b); return f.prepend("{"), f.add("}"), f }, generateList: function (a) { for (var b = this.empty(), c = 0, e = a.length; e > c; c++) c && b.add(","), b.add(d(a[c], this)); return b }, generateArray: function (a) { var b = this.generateList(a); return b.prepend("["), b.add("]"), b } }, b["default"] = e, a.exports = b["default"] }, function (a, b, c) { "use strict"; function d(a) { g["default"](a), i["default"](a), k["default"](a), m["default"](a), o["default"](a), q["default"](a), s["default"](a) } var e = c(8)["default"]; b.__esModule = !0, b.registerDefaultHelpers = d; var f = c(22), g = e(f), h = c(23), i = e(h), j = c(24), k = e(j), l = c(25), m = e(l), n = c(26), o = e(n), p = c(27), q = e(p), r = c(28), s = e(r) }, function (a, b, c) { "use strict"; function d(a) { g["default"](a) } var e = c(8)["default"]; b.__esModule = !0, b.registerDefaultDecorators = d; var f = c(29), g = e(f) }, function (a, b, c) { "use strict"; b.__esModule = !0; var d = c(13), e = { methodMap: ["debug", "info", "warn", "error"], level: "info", lookupLevel: function (a) { if ("string" == typeof a) { var b = d.indexOf(e.methodMap, a.toLowerCase()); a = b >= 0 ? b : parseInt(a, 10) } return a }, log: function (a) { if (a = e.lookupLevel(a), "undefined" != typeof console && e.lookupLevel(e.level) <= a) { var b = e.methodMap[a]; console[b] || (b = "log"); for (var c = arguments.length, d = Array(c > 1 ? c - 1 : 0), f = 1; c > f; f++) d[f - 1] = arguments[f]; console[b].apply(console, d) } } }; b["default"] = e, a.exports = b["default"] }, function (a, b, c) { "use strict"; b.__esModule = !0; var d = c(13); b["default"] = function (a) { a.registerHelper("blockHelperMissing", function (b, c) { var e = c.inverse, f = c.fn; if (b === !0) return f(this); if (b === !1 || null == b) return e(this); if (d.isArray(b)) return b.length > 0 ? (c.ids && (c.ids = [c.name]), a.helpers.each(b, c)) : e(this); if (c.data && c.ids) { var g = d.createFrame(c.data); g.contextPath = d.appendContextPath(c.data.contextPath, c.name), c = { data: g } } return f(b, c) }) }, a.exports = b["default"] }, function (a, b, c) { "use strict"; var d = c(8)["default"]; b.__esModule = !0; var e = c(13), f = c(12), g = d(f); b["default"] = function (a) { a.registerHelper("each", function (a, b) { function c(b, c, f) { j && (j.key = b, j.index = c, j.first = 0 === c, j.last = !!f, k && (j.contextPath = k + b)), i += d(a[b], { data: j, blockParams: e.blockParams([a[b], b], [k + b, null]) }) } if (!b) throw new g["default"]("Must pass iterator to #each"); var d = b.fn, f = b.inverse, h = 0, i = "", j = void 0, k = void 0; if (b.data && b.ids && (k = e.appendContextPath(b.data.contextPath, b.ids[0]) + "."), e.isFunction(a) && (a = a.call(this)), b.data && (j = e.createFrame(b.data)), a && "object" == typeof a) if (e.isArray(a)) for (var l = a.length; l > h; h++) h in a && c(h, h, h === a.length - 1); else { var m = void 0; for (var n in a) a.hasOwnProperty(n) && (void 0 !== m && c(m, h - 1), m = n, h++); void 0 !== m && c(m, h - 1, !0) } return 0 === h && (i = f(this)), i }) }, a.exports = b["default"] }, function (a, b, c) { "use strict"; var d = c(8)["default"]; b.__esModule = !0; var e = c(12), f = d(e); b["default"] = function (a) { a.registerHelper("helperMissing", function () { if (1 === arguments.length) return void 0; throw new f["default"]('Missing helper: "' + arguments[arguments.length - 1].name + '"') }) }, a.exports = b["default"] }, function (a, b, c) { "use strict"; b.__esModule = !0; var d = c(13); b["default"] = function (a) { a.registerHelper("if", function (a, b) { return d.isFunction(a) && (a = a.call(this)), !b.hash.includeZero && !a || d.isEmpty(a) ? b.inverse(this) : b.fn(this) }), a.registerHelper("unless", function (b, c) { return a.helpers["if"].call(this, b, { fn: c.inverse, inverse: c.fn, hash: c.hash }) }) }, a.exports = b["default"] }, function (a, b, c) { "use strict"; b.__esModule = !0, b["default"] = function (a) { a.registerHelper("log", function () { for (var b = [void 0], c = arguments[arguments.length - 1], d = 0; d < arguments.length - 1; d++) b.push(arguments[d]); var e = 1; null != c.hash.level ? e = c.hash.level : c.data && null != c.data.level && (e = c.data.level), b[0] = e, a.log.apply(a, b) }) }, a.exports = b["default"] }, function (a, b, c) { "use strict"; b.__esModule = !0, b["default"] = function (a) { a.registerHelper("lookup", function (a, b) { return a && a[b] }) }, a.exports = b["default"] }, function (a, b, c) { "use strict"; b.__esModule = !0; var d = c(13); b["default"] = function (a) { a.registerHelper("with", function (a, b) { d.isFunction(a) && (a = a.call(this)); var c = b.fn; if (d.isEmpty(a)) return b.inverse(this); var e = b.data; return b.data && b.ids && (e = d.createFrame(b.data), e.contextPath = d.appendContextPath(b.data.contextPath, b.ids[0])), c(a, { data: e, blockParams: d.blockParams([a], [e && e.contextPath]) }) }) }, a.exports = b["default"] }, function (a, b, c) { "use strict"; b.__esModule = !0; var d = c(13); b["default"] = function (a) { a.registerDecorator("inline", function (a, b, c, e) { var f = a; return b.partials || (b.partials = {}, f = function (e, f) { var g = c.partials; c.partials = d.extend({}, g, b.partials); var h = a(e, f); return c.partials = g, h }), b.partials[e.args[0]] = e.fn, f }) }, a.exports = b["default"] }])
});


Handlebars.registerHelper("dateFormat", function (e, r) { if (window.moment) { var s = r.hash.format || "MMM DD, YYYY hh:mm:ss A"; return moment(e).format(s) } return e }), Handlebars.registerHelper("ifCond", function (e, r, s, t) { switch (r) { case "==": return e == s ? t.fn(this) : t.inverse(this); case "===": return e === s ? t.fn(this) : t.inverse(this); case "!==": return e !== s ? t.fn(this) : t.inverse(this); case "<": return s > e ? t.fn(this) : t.inverse(this); case "<=": return s >= e ? t.fn(this) : t.inverse(this); case ">": return e > s ? t.fn(this) : t.inverse(this); case ">=": return e >= s ? t.fn(this) : t.inverse(this); case "&&": return e && s ? t.fn(this) : t.inverse(this); case "||": return e || s ? t.fn(this) : t.inverse(this); default: return t.inverse(this) } });



(function (factory) {

    if (typeof define === 'function' && define.amd) {

        // AMD. Register as an anonymous module.
        define(['jquery', 'moment'], factory);
    } else if (typeof exports === 'object') {

        // Node/CommonJS
        factory(require('jquery'), require('moment'));
    } else {

        // Browser globals
        factory(jQuery, moment);
    }

}(function ($, moment) {

    // This is the default calendar template. This can be overridden.
    var clndrTemplate = "<div class='clndr-controls'>" +
      "<div class='clndr-control-button'><span class='clndr-previous-button'>previous</span></div><div class='month'><%= month %> <%= year %></div><div class='clndr-control-button rightalign'><span class='clndr-next-button'>next</span></div>" +
      "</div>" +
      "<table class='clndr-table' border='0' cellspacing='0' cellpadding='0'>" +
      "<thead>" +
      "<tr class='header-days'>" +
      "<% for(var i = 0; i < daysOfTheWeek.length; i++) { %>" +
        "<td class='header-day'><%= daysOfTheWeek[i] %></td>" +
      "<% } %>" +
      "</tr>" +
      "</thead>" +
      "<tbody>" +
      "<% for(var i = 0; i < numberOfRows; i++){ %>" +
        "<tr>" +
        "<% for(var j = 0; j < 7; j++){ %>" +
        "<% var d = j + i * 7; %>" +
        "<td class='<%= days[d].classes %>'><div class='day-contents'><%= days[d].day %>" +
        "</div></td>" +
        "<% } %>" +
        "</tr>" +
      "<% } %>" +
      "</tbody>" +
    "</table>";

    var pluginName = 'clndr';

    var defaults = {
        template: clndrTemplate,
        weekOffset: 0,
        startWithMonth: null,
        clickEvents: {
            click: null,
            nextMonth: null,
            previousMonth: null,
            nextYear: null,
            previousYear: null,
            today: null,
            onMonthChange: null,
            onYearChange: null
        },
        targets: {
            nextButton: 'clndr-next-button',
            previousButton: 'clndr-previous-button',
            nextYearButton: 'clndr-next-year-button',
            previousYearButton: 'clndr-previous-year-button',
            todayButton: 'clndr-today-button',
            day: 'day',
            empty: 'empty'
        },
        classes: {
            today: "today",
            event: "event",
            past: "past",
            lastMonth: "last-month",
            nextMonth: "next-month",
            adjacentMonth: "adjacent-month",
            inactive: "inactive",
            selected: "selected"
        },
        events: [],
        extras: null,
        dateParameter: 'date',
        multiDayEvents: null,
        doneRendering: null,
        render: null,
        daysOfTheWeek: null,
        showAdjacentMonths: true,
        adjacentDaysChangeMonth: false,
        ready: null,
        constraints: null,
        forceSixRows: null,
        trackSelectedDate: false,
        selectedDate: null,
        ignoreInactiveDaysInSelection: null,
        lengthOfTime: {
            months: null,
            days: null,
            interval: 1
        },
        moment: null
    };

    // The actual plugin constructor
    function Clndr(element, options) {
        this.element = element;

        // merge the default options with user-provided options
        this.options = $.extend(true, {}, defaults, options);

        // check if moment was passed in as a dependency
        if (this.options.moment) {
            moment = this.options.moment;
        }

        // if there are events, we should run them through our addMomentObjectToEvents function
        // which will add a date object that we can use to make life easier. This is only necessary
        // when events are provided on instantiation, since our setEvents function uses addMomentObjectToEvents.
        if (this.options.events.length) {
            if (this.options.multiDayEvents) {
                this.options.events = this.addMultiDayMomentObjectsToEvents(this.options.events);
            } else {
                this.options.events = this.addMomentObjectToEvents(this.options.events);
            }
        }

        // this used to be a place where we'd figure out the current month, but since
        // we want to open up support for arbitrary lengths of time we're going to
        // store the current range in addition to the current month.
        if (this.options.lengthOfTime.months || this.options.lengthOfTime.days) {
            // we want to establish intervalStart and intervalEnd, which will keep track
            // of our boundaries. Let's look at the possibilities...
            if (this.options.lengthOfTime.months) {
                // gonna go right ahead and annihilate any chance for bugs here.
                this.options.lengthOfTime.days = null;
                // the length is specified in months. Is there a start date?
                if (this.options.lengthOfTime.startDate) {
                    this.intervalStart = moment(this.options.lengthOfTime.startDate).startOf('month');
                } else if (this.options.startWithMonth) {
                    this.intervalStart = moment(this.options.startWithMonth).startOf('month');
                } else {
                    this.intervalStart = moment().startOf('month');
                }
                // subtract a day so that we are at the end of the interval. We always
                // want intervalEnd to be inclusive.
                this.intervalEnd = moment(this.intervalStart).add(this.options.lengthOfTime.months, 'months').subtract(1, 'days');
                this.month = this.intervalStart.clone();
            } else if (this.options.lengthOfTime.days) {
                // the length is specified in days. Start date?
                if (this.options.lengthOfTime.startDate) {
                    this.intervalStart = moment(this.options.lengthOfTime.startDate).startOf('day');
                } else {
                    this.intervalStart = moment().weekday(0).startOf('day');
                }
                this.intervalEnd = moment(this.intervalStart).add(this.options.lengthOfTime.days - 1, 'days').endOf('day');
                this.month = this.intervalStart.clone();
            }
        } else {
            this.month = moment().startOf('month');
            this.intervalStart = moment(this.month);
            this.intervalEnd = moment(this.month).endOf('month');
        }

        if (this.options.startWithMonth) {
            this.month = moment(this.options.startWithMonth).startOf('month');
            this.intervalStart = moment(this.month);
            this.intervalEnd = moment(this.month).endOf('month');
        }

        // if we've got constraints set, make sure the interval is within them.
        if (this.options.constraints) {
            // first check if the start date exists & is later than now.
            if (this.options.constraints.startDate) {
                var startMoment = moment(this.options.constraints.startDate);
                if (this.intervalStart.isBefore(startMoment, 'month')) {
                    // try to preserve the date by moving only the month...
                    this.intervalStart.set('month', startMoment.month()).set('year', startMoment.year());
                    this.month.set('month', startMoment.month()).set('year', startMoment.year());
                }
            }
            // make sure the intervalEnd is before the endDate
            if (this.options.constraints.endDate) {
                var endMoment = moment(this.options.constraints.endDate);
                if (this.intervalEnd.isAfter(endMoment, 'month')) {
                    this.intervalEnd.set('month', endMoment.month()).set('year', endMoment.year());
                    this.month.set('month', endMoment.month()).set('year', endMoment.year());
                }
            }
        }

        this._defaults = defaults;
        this._name = pluginName;

        // Some first-time initialization -> day of the week offset,
        // template compiling, making and storing some elements we'll need later,
        // & event handling for the controller.
        this.init();
    }

    Clndr.prototype.init = function () {
        // create the days of the week using moment's current language setting
        this.daysOfTheWeek = this.options.daysOfTheWeek || [];
        if (!this.options.daysOfTheWeek) {
            this.daysOfTheWeek = [];
            for (var i = 0; i < 7; i++) {
                this.daysOfTheWeek.push(moment().weekday(i).format('dd').charAt(0));
            }
        }
        // shuffle the week if there's an offset
        if (this.options.weekOffset) {
            this.daysOfTheWeek = this.shiftWeekdayLabels(this.options.weekOffset);
        }

        // quick & dirty test to make sure rendering is possible.
        if (!$.isFunction(this.options.render)) {
            this.options.render = null;
            if (typeof _ === 'undefined') {
                throw new Error("Underscore was not found. Please include underscore.js OR provide a custom render function.");
            }
            else {
                // we're just going ahead and using underscore here if no render method has been supplied.
                this.compiledClndrTemplate = _.template(this.options.template);
            }
        }

        // create the parent element that will hold the plugin & save it for later
        $(this.element).html("<div class='clndr'></div>");
        this.calendarContainer = $('.clndr', this.element);

        // attach event handlers for clicks on buttons/cells
        this.bindEvents();

        // do a normal render of the calendar template
        this.render();

        // if a ready callback has been provided, call it.
        if (this.options.ready) {
            this.options.ready.apply(this, []);
        }
    };

    Clndr.prototype.shiftWeekdayLabels = function (offset) {
        var days = this.daysOfTheWeek;
        for (var i = 0; i < offset; i++) {
            days.push(days.shift());
        }
        return days;
    };

    // This is where the magic happens. Given a starting date and ending date,
    // an array of calendarDay objects is constructed that contains appropriate
    // events and classes depending on the circumstance.
    Clndr.prototype.createDaysObject = function (startDate, endDate) {
        // this array will hold numbers for the entire grid (even the blank spaces)
        var daysArray = [];
        var date = startDate.clone();
        var lengthOfInterval = endDate.diff(startDate, 'days');

        // this is a helper object so that days can resolve their classes correctly.
        // Don't use it for anything please.
        this._currentIntervalStart = startDate.clone();

        // filter the events list (if it exists) to events that are happening last month, this month and next month (within the current grid view)
        this.eventsLastMonth = [];
        this.eventsThisInterval = [];
        this.eventsNextMonth = [];

        if (this.options.events.length) {

            // EVENT PARSING
            // here are the only two cases where we don't get an event in our interval:
            // startDate | endDate   | e.start   | e.end
            // e.start   | e.end     | startDate | endDate
            this.eventsThisInterval = $(this.options.events).filter(function () {
                if (
                  this._clndrEndDateObject.isBefore(startDate) ||
                  this._clndrStartDateObject.isAfter(endDate)
                ) {
                    return false;
                } else {
                    return true;
                }
            }).toArray();

            if (this.options.showAdjacentMonths) {
                var startOfLastMonth = startDate.clone().subtract(1, 'months').startOf('month');
                var endOfLastMonth = startOfLastMonth.clone().endOf('month');
                var startOfNextMonth = endDate.clone().add(1, 'months').startOf('month');
                var endOfNextMonth = startOfNextMonth.clone().endOf('month');

                this.eventsLastMonth = $(this.options.events).filter(function () {
                    if (
                      this._clndrEndDateObject.isBefore(startOfLastMonth) ||
                      this._clndrStartDateObject.isAfter(endOfLastMonth)
                    ) {
                        return false;
                    } else {
                        return true;
                    }
                }).toArray();

                this.eventsNextMonth = $(this.options.events).filter(function () {
                    if (
                      this._clndrEndDateObject.isBefore(startOfNextMonth) ||
                      this._clndrStartDateObject.isAfter(endOfNextMonth)
                    ) {
                        return false;
                    } else {
                        return true;
                    }
                }).toArray();
            }
        }

        // if diff is greater than 0, we'll have to fill in last days of the previous month
        // to account for the empty boxes in the grid.
        // we also need to take into account the weekOffset parameter.
        // None of this needs to happen if the interval is being specified in days rather than months.
        if (!this.options.lengthOfTime.days) {
            var diff = date.weekday() - this.options.weekOffset;
            if (diff < 0) diff += 7;

            if (this.options.showAdjacentMonths) {
                for (var i = 0; i < diff; i++) {
                    var day = moment([startDate.year(), startDate.month(), i - diff + 1]);
                    daysArray.push(this.createDayObject(day, this.eventsLastMonth));
                }
            } else {
                for (var i = 0; i < diff; i++) {
                    daysArray.push(this.calendarDay({
                        classes: this.options.targets.empty + " " + this.options.classes.lastMonth
                    }));
                }
            }
        }

        // now we push all of the days in the interval
        var dateIterator = startDate.clone();
        while (dateIterator.isBefore(endDate) || dateIterator.isSame(endDate, 'day')) {
            daysArray.push(this.createDayObject(dateIterator.clone(), this.eventsThisInterval));
            dateIterator.add(1, 'days');
        }

        // ...and if there are any trailing blank boxes, fill those in
        // with the next month first days.
        // Again, we can ignore this if the interval is specified in days.
        if (!this.options.lengthOfTime.days) {
            while (daysArray.length % 7 !== 0) {
                if (this.options.showAdjacentMonths) {
                    daysArray.push(this.createDayObject(dateIterator.clone(), this.eventsNextMonth));
                } else {
                    daysArray.push(this.calendarDay({
                        classes: this.options.targets.empty + " " + this.options.classes.nextMonth
                    }));
                }
                dateIterator.add(1, 'days');
            }
        }

        // if we want to force six rows of calendar, now's our Last Chance to add another row.
        // if the 42 seems explicit it's because we're creating a 7-row grid and 6 rows of 7 is always 42!
        if (this.options.forceSixRows && daysArray.length !== 42) {
            while (daysArray.length < 42) {
                if (this.options.showAdjacentMonths) {
                    daysArray.push(this.createDayObject(dateIterator.clone(), this.eventsNextMonth));
                    dateIterator.add(1, 'days');
                } else {
                    daysArray.push(this.calendarDay({
                        classes: this.options.targets.empty + " " + this.options.classes.nextMonth
                    }));
                }
            }
        }

        return daysArray;
    };

    Clndr.prototype.createDayObject = function (day, monthEvents) {
        var eventsToday = [];
        var now = moment();
        var self = this;

        // validate moment date
        if (!day.isValid() && day.hasOwnProperty('_d') && day._d != undefined) {
            day = moment(day._d);
        }

        var j = 0, l = monthEvents.length;
        for (j; j < l; j++) {
            // keep in mind that the events here already passed the month/year test.
            // now all we have to compare is the moment.date(), which returns the day of the month.
            var start = monthEvents[j]._clndrStartDateObject;
            var end = monthEvents[j]._clndrEndDateObject;
            // if today is the same day as start or is after the start, and
            // if today is the same day as the end or before the end ...
            // woohoo semantics!
            if ((day.isSame(start, 'day') || day.isAfter(start, 'day')) &&
              (day.isSame(end, 'day') || day.isBefore(end, 'day'))) {
                eventsToday.push(monthEvents[j]);
            }
        }

        var properties = {
            isInactive: false,
            isAdjacentMonth: false,
            isToday: false
        };
        var extraClasses = "";

        if (now.format("YYYY-MM-DD") == day.format("YYYY-MM-DD")) {
            extraClasses += (" " + this.options.classes.today);
            properties.isToday = true;
        }
        if (day.isBefore(now, 'day')) {
            extraClasses += (" " + this.options.classes.past);
        }
        if (eventsToday.length) {
            extraClasses += (" " + this.options.classes.event);
        }
        if (!this.options.lengthOfTime.days) {
            if (this._currentIntervalStart.month() > day.month()) {
                extraClasses += (" " + this.options.classes.adjacentMonth);
                properties.isAdjacentMonth = true;

                this._currentIntervalStart.year() === day.year()
                    ? extraClasses += (" " + this.options.classes.lastMonth)
                    : extraClasses += (" " + this.options.classes.nextMonth);

            } else if (this._currentIntervalStart.month() < day.month()) {
                extraClasses += (" " + this.options.classes.adjacentMonth);
                properties.isAdjacentMonth = true;

                this._currentIntervalStart.year() === day.year()
                    ? extraClasses += (" " + this.options.classes.nextMonth)
                    : extraClasses += (" " + this.options.classes.lastMonth);
            }
        }

        // if there are constraints, we need to add the inactive class to the days outside of them
        if (this.options.constraints) {
            if (this.options.constraints.startDate && day.isBefore(moment(this.options.constraints.startDate))) {
                extraClasses += (" " + this.options.classes.inactive);
                properties.isInactive = true;
            }
            if (this.options.constraints.endDate && day.isAfter(moment(this.options.constraints.endDate))) {
                extraClasses += (" " + this.options.classes.inactive);
                properties.isInactive = true;
            }
        }

        // validate moment date
        if (!day.isValid() && day.hasOwnProperty('_d') && day._d != undefined) {
            day = moment(day._d);
        }

        // check whether the day is "selected"
        if (this.options.selectedDate && day.isSame(moment(this.options.selectedDate), 'day')) {
            extraClasses += (" " + this.options.classes.selected);
        }

        // we're moving away from using IDs in favor of classes, since when
        // using multiple calendars on a page we are technically violating the
        // uniqueness of IDs.
        extraClasses += " calendar-day-" + day.format("YYYY-MM-DD");

        // day of week
        extraClasses += " calendar-dow-" + day.weekday();

        return this.calendarDay({
            day: day.date(),
            classes: this.options.targets.day + extraClasses,
            events: eventsToday,
            date: day,
            properties: properties
        });
    };

    Clndr.prototype.render = function () {
        // get rid of the previous set of calendar parts.
        // this should handle garbage collection according to jQuery's docs:
        // http://api.jquery.com/empty/
        //  > To avoid memory leaks, jQuery removes other constructs such as
        //  > data and event handlers from the child elements before removing
        //  > the elements themselves.
        this.calendarContainer.empty();

        var data = {};

        if (this.options.lengthOfTime.days) {
            var days = this.createDaysObject(this.intervalStart.clone(), this.intervalEnd.clone());

            data = {
                daysOfTheWeek: this.daysOfTheWeek,
                numberOfRows: Math.ceil(days.length / 7),
                months: [],
                days: days,
                month: null,
                year: null,
                intervalStart: this.intervalStart.clone(),
                intervalEnd: this.intervalEnd.clone(),
                eventsThisInterval: this.eventsThisInterval,
                eventsLastMonth: [],
                eventsNextMonth: [],
                extras: this.options.extras
            };

        } else if (this.options.lengthOfTime.months) {

            var months = [];
            var eventsThisInterval = [];

            for (i = 0; i < this.options.lengthOfTime.months; i++) {
                var currentIntervalStart = this.intervalStart.clone().add(i, 'months');
                var currentIntervalEnd = currentIntervalStart.clone().endOf('month');
                var days = this.createDaysObject(currentIntervalStart, currentIntervalEnd);
                // save events processed for each month into a master array of events for
                // this interval
                eventsThisInterval.push(this.eventsThisInterval);
                months.push({
                    month: currentIntervalStart,
                    days: days
                });
            }

            data = {
                daysOfTheWeek: this.daysOfTheWeek,
                numberOfRows: _.reduce(months, function (memo, monthObj) {
                    return memo + Math.ceil(monthObj.days.length / 7);
                }, 0),
                months: months,
                days: [],
                month: null,
                year: null,
                intervalStart: this.intervalStart,
                intervalEnd: this.intervalEnd,
                eventsThisInterval: eventsThisInterval,
                eventsLastMonth: this.eventsLastMonth,
                eventsNextMonth: this.eventsNextMonth,
                extras: this.options.extras
            };
        } else {
            // get an array of days and blank spaces
            var days = this.createDaysObject(this.month.clone().startOf('month'), this.month.clone().endOf('month'));
            // this is to prevent a scope/naming issue between this.month and data.month
            var currentMonth = this.month;

            var data = {
                daysOfTheWeek: this.daysOfTheWeek,
                numberOfRows: Math.ceil(days.length / 7),
                months: [],
                days: days,
                month: this.month.format('MMMM'),
                year: this.month.year(),
                eventsThisMonth: this.eventsThisInterval,
                eventsLastMonth: this.eventsLastMonth,
                eventsNextMonth: this.eventsNextMonth,
                extras: this.options.extras
            };
        }

        // render the calendar with the data above & bind events to its elements
        if (!this.options.render) {
            this.calendarContainer.html(this.compiledClndrTemplate(data));
        } else {
            this.calendarContainer.html(this.options.render.apply(this, [data]));
        }

        // if there are constraints, we need to add the 'inactive' class to the controls
        if (this.options.constraints) {
            // in the interest of clarity we're just going to remove all inactive classes and re-apply them each render.
            for (var target in this.options.targets) {
                if (target != this.options.targets.day) {
                    this.element.find('.' + this.options.targets[target]).toggleClass(this.options.classes.inactive, false);
                }
            }

            var start = null;
            var end = null;

            if (this.options.constraints.startDate) {
                start = moment(this.options.constraints.startDate);
            }
            if (this.options.constraints.endDate) {
                end = moment(this.options.constraints.endDate);
            }
            // deal with the month controls first.
            // do we have room to go back?
            if (start && (start.isAfter(this.intervalStart) || start.isSame(this.intervalStart, 'day'))) {
                this.element.find('.' + this.options.targets.previousButton).toggleClass(this.options.classes.inactive, true);
            }
            // do we have room to go forward?
            if (end && (end.isBefore(this.intervalEnd) || end.isSame(this.intervalEnd, 'day'))) {
                this.element.find('.' + this.options.targets.nextButton).toggleClass(this.options.classes.inactive, true);
            }
            // what's last year looking like?
            if (start && start.isAfter(this.intervalStart.clone().subtract(1, 'years'))) {
                this.element.find('.' + this.options.targets.previousYearButton).toggleClass(this.options.classes.inactive, true);
            }
            // how about next year?
            if (end && end.isBefore(this.intervalEnd.clone().add(1, 'years'))) {
                this.element.find('.' + this.options.targets.nextYearButton).toggleClass(this.options.classes.inactive, true);
            }
            // today? we could put this in init(), but we want to support the user changing the constraints on a living instance.
            if ((start && start.isAfter(moment(), 'month')) || (end && end.isBefore(moment(), 'month'))) {
                this.element.find('.' + this.options.targets.today).toggleClass(this.options.classes.inactive, true);
            }
        }

        if (this.options.doneRendering) {
            this.options.doneRendering.apply(this, []);
        }
    };

    Clndr.prototype.bindEvents = function () {
        var $container = $(this.element);
        var self = this;
        var eventType = 'click';
        if (self.options.useTouchEvents === true) {
            eventType = 'touchstart';
        }

        // Make sure we don't already have events
        $container.off(eventType + '.clndr', '.' + this.options.targets.day)
          .off(eventType + '.clndr', '.' + this.options.targets.empty)
          .off(eventType + '.clndr', '.' + this.options.targets.previousButton)
          .off(eventType + '.clndr', '.' + this.options.targets.nextButton)
          .off(eventType + '.clndr', '.' + this.options.targets.todayButton)
          .off(eventType + '.clndr', '.' + this.options.targets.nextYearButton)
          .off(eventType + '.clndr', '.' + this.options.targets.previousYearButton);

        // target the day elements and give them click events
        $container.on(eventType + '.clndr', '.' + this.options.targets.day, function (event) {
            if (self.options.clickEvents.click) {
                var target = self.buildTargetObject(event.currentTarget, true);
                self.options.clickEvents.click.apply(self, [target]);
            }
            // if adjacentDaysChangeMonth is on, we need to change the month here.
            if (self.options.adjacentDaysChangeMonth) {
                if ($(event.currentTarget).is('.' + self.options.classes.lastMonth)) {
                    self.backActionWithContext(self);
                } else if ($(event.currentTarget).is('.' + self.options.classes.nextMonth)) {
                    self.forwardActionWithContext(self);
                }
            }
            // if trackSelectedDate is on, we need to handle click on a new day
            if (self.options.trackSelectedDate) {
                if (self.options.ignoreInactiveDaysInSelection && $(event.currentTarget).hasClass('inactive')) {
                    return;
                }
                // remember new selected date
                self.options.selectedDate = self.getTargetDateString(event.currentTarget);

                // handle "selected" class
                $(event.currentTarget)
                  .siblings().removeClass(self.options.classes.selected).end()
                  .addClass(self.options.classes.selected);
            }
        });
        // target the empty calendar boxes as well
        $container.on(eventType + '.clndr', '.' + this.options.targets.empty, function (event) {
            if (self.options.clickEvents.click) {
                var target = self.buildTargetObject(event.currentTarget, false);
                self.options.clickEvents.click.apply(self, [target]);
            }
            if (self.options.adjacentDaysChangeMonth) {
                if ($(event.currentTarget).is('.' + self.options.classes.lastMonth)) {
                    self.backActionWithContext(self);
                } else if ($(event.currentTarget).is('.' + self.options.classes.nextMonth)) {
                    self.forwardActionWithContext(self);
                }
            }
        });

        // bind the previous, next and today buttons
        $container
          .on(eventType + '.clndr', '.' + this.options.targets.previousButton, { context: this }, this.backAction)
          .on(eventType + '.clndr', '.' + this.options.targets.nextButton, { context: this }, this.forwardAction)
          .on(eventType + '.clndr', '.' + this.options.targets.todayButton, { context: this }, this.todayAction)
          .on(eventType + '.clndr', '.' + this.options.targets.nextYearButton, { context: this }, this.nextYearAction)
          .on(eventType + '.clndr', '.' + this.options.targets.previousYearButton, { context: this }, this.previousYearAction);
    }

    // If the user provided a click callback we'd like to give them something nice to work with.
    // buildTargetObject takes the DOM element that was clicked and returns an object with
    // the DOM element, events, and the date (if the latter two exist). Currently it is based on the id,
    // however it'd be nice to use a data- attribute in the future.
    Clndr.prototype.buildTargetObject = function (currentTarget, targetWasDay) {
        // This is our default target object, assuming we hit an empty day with no events.
        var target = {
            element: currentTarget,
            events: [],
            date: null
        };
        // did we click on a day or just an empty box?
        if (targetWasDay) {
            var dateString = this.getTargetDateString(currentTarget);
            target.date = (dateString) ? moment(dateString) : null;

            // do we have events?
            if (this.options.events) {
                // are any of the events happening today?
                if (this.options.multiDayEvents) {
                    target.events = $.makeArray($(this.options.events).filter(function () {
                        // filter the dates down to the ones that match.
                        return ((target.date.isSame(this._clndrStartDateObject, 'day') || target.date.isAfter(this._clndrStartDateObject, 'day')) &&
                          (target.date.isSame(this._clndrEndDateObject, 'day') || target.date.isBefore(this._clndrEndDateObject, 'day')));
                    }));
                } else {
                    target.events = $.makeArray($(this.options.events).filter(function () {
                        // filter the dates down to the ones that match.
                        return this._clndrStartDateObject.format('YYYY-MM-DD') == dateString;
                    }));
                }
            }
        }

        return target;
    }

    // get moment date object of the date associated with the given target.
    // this method is meant to be called on ".day" elements.
    Clndr.prototype.getTargetDateString = function (target) {
        // Our identifier is in the list of classNames. Find it!
        var classNameIndex = target.className.indexOf('calendar-day-');
        if (classNameIndex !== -1) {
            // our unique identifier is always 23 characters long.
            // If this feels a little wonky, that's probably because it is.
            // Open to suggestions on how to improve this guy.
            return target.className.substring(classNameIndex + 13, classNameIndex + 23);
        }

        return null;
    }

    // the click handlers in bindEvents need a context, so these are wrappers
    // to the actual functions. Todo: better way to handle this?
    Clndr.prototype.forwardAction = function (event) {
        var self = event.data.context;
        self.forwardActionWithContext(self);
    };

    Clndr.prototype.backAction = function (event) {
        var self = event.data.context;
        self.backActionWithContext(self);
    };

    // These are called directly, except for in the bindEvent click handlers,
    // where forwardAction and backAction proxy to these guys.
    Clndr.prototype.backActionWithContext = function (self) {
        // before we do anything, check if there is an inactive class on the month control.
        // if it does, we want to return and take no action.
        if (self.element.find('.' + self.options.targets.previousButton).hasClass('inactive')) {
            return;
        }

        var yearChanged = null;

        if (!self.options.lengthOfTime.days) {
            // shift the interval by a month (or several months)
            self.intervalStart.subtract(self.options.lengthOfTime.interval, 'months').startOf('month');
            self.intervalEnd = self.intervalStart.clone().add(self.options.lengthOfTime.months || self.options.lengthOfTime.interval, 'months').subtract(1, 'days').endOf('month');

            if (!self.options.lengthOfTime.months) {
                yearChanged = !self.month.isSame(moment(self.month).subtract(1, 'months'), 'year');
            }

            self.month = self.intervalStart.clone();
        } else {
            // shift the interval in days
            self.intervalStart.subtract(self.options.lengthOfTime.interval, 'days').startOf('day');
            self.intervalEnd = self.intervalStart.clone().add(self.options.lengthOfTime.days - 1, 'days').endOf('day');
            // this is useless, i think, but i'll keep it as a precaution for now
            self.month = self.intervalStart.clone();
        }

        self.render();

        if (!self.options.lengthOfTime.days && !self.options.lengthOfTime.months) {
            if (self.options.clickEvents.previousMonth) {
                self.options.clickEvents.previousMonth.apply(self, [moment(self.month)]);
            }
            if (self.options.clickEvents.onMonthChange) {
                self.options.clickEvents.onMonthChange.apply(self, [moment(self.month)]);
            }
            if (yearChanged) {
                if (self.options.clickEvents.onYearChange) {
                    self.options.clickEvents.onYearChange.apply(self, [moment(self.month)]);
                }
            }
        } else {
            if (self.options.clickEvents.previousInterval) {
                self.options.clickEvents.previousInterval.apply(self, [moment(self.intervalStart), moment(self.intervalEnd)]);
            }
            if (self.options.clickEvents.onIntervalChange) {
                self.options.clickEvents.onIntervalChange.apply(self, [moment(self.intervalStart), moment(self.intervalEnd)]);
            }
        }
    };

    Clndr.prototype.forwardActionWithContext = function (self) {
        // before we do anything, check if there is an inactive class on the month control.
        // if it does, we want to return and take no action.
        if (self.element.find('.' + self.options.targets.nextButton).hasClass('inactive')) {
            return;
        }

        var yearChanged = null;

        if (!self.options.lengthOfTime.days) {
            // shift the interval by a month (or several months)
            self.intervalStart.add(self.options.lengthOfTime.interval, 'months').startOf('month');
            self.intervalEnd = self.intervalStart.clone().add(self.options.lengthOfTime.months || self.options.lengthOfTime.interval, 'months').subtract(1, 'days').endOf('month');

            if (!self.options.lengthOfTime.months) {
                yearChanged = !self.month.isSame(moment(self.month).add(1, 'months'), 'year');
            }

            self.month = self.intervalStart.clone();
        } else {
            // shift the interval in days
            self.intervalStart.add(self.options.lengthOfTime.interval, 'days').startOf('day');
            self.intervalEnd = self.intervalStart.clone().add(self.options.lengthOfTime.days - 1, 'days').endOf('day');
            // this is useless, i think, but i'll keep it as a precaution for now
            self.month = self.intervalStart.clone();
        }

        self.render();

        if (!self.options.lengthOfTime.days && !self.options.lengthOfTime.months) {
            if (self.options.clickEvents.nextMonth) {
                self.options.clickEvents.nextMonth.apply(self, [moment(self.month)]);
            }
            if (self.options.clickEvents.onMonthChange) {
                self.options.clickEvents.onMonthChange.apply(self, [moment(self.month)]);
            }
            if (yearChanged) {
                if (self.options.clickEvents.onYearChange) {
                    self.options.clickEvents.onYearChange.apply(self, [moment(self.month)]);
                }
            }
        } else {
            if (self.options.clickEvents.nextInterval) {
                self.options.clickEvents.nextInterval.apply(self, [moment(self.intervalStart), moment(self.intervalEnd)]);
            }
            if (self.options.clickEvents.onIntervalChange) {
                self.options.clickEvents.onIntervalChange.apply(self, [moment(self.intervalStart), moment(self.intervalEnd)]);
            }
        }
    };

    Clndr.prototype.todayAction = function (event) {
        var self = event.data.context;

        // did we switch months when the today button was hit?
        var monthChanged = !self.month.isSame(moment(), 'month');
        var yearChanged = !self.month.isSame(moment(), 'year');

        self.month = moment().startOf('month');

        if (self.options.lengthOfTime.days) {
            // if there was a startDate specified, we should figure out what the weekday is and
            // use that as the starting point of our interval. If not, go to today.weekday(0)
            if (self.options.lengthOfTime.startDate) {
                self.intervalStart = moment().weekday(self.options.lengthOfTime.startDate.weekday()).startOf('day');
            } else {
                self.intervalStart = moment().weekday(0).startOf('day');
            }
            self.intervalEnd = self.intervalStart.clone().add(self.options.lengthOfTime.days - 1, 'days').endOf('day');

        } else if (self.options.lengthOfTime.months) {
            // set the intervalStart to this month.
            self.intervalStart = moment().startOf('month');
            self.intervalEnd = self.intervalStart.clone()
              .add(self.options.lengthOfTime.months || self.options.lengthOfTime.interval, 'months')
              .subtract(1, 'days')
              .endOf('month');
        } else if (monthChanged) {
            // reset the start interval for the current month
            self.intervalStart = moment().startOf('month');
            // no need to re-render if we didn't change months.
            self.render();

            // fire the today event handler regardless of whether the month changed.
            if (self.options.clickEvents.today) {
                self.options.clickEvents.today.apply(self, [moment(self.month)]);
            }

            // fire the onMonthChange callback
            if (self.options.clickEvents.onMonthChange) {
                self.options.clickEvents.onMonthChange.apply(self, [moment(self.month)]);
            }
            // maybe fire the onYearChange callback?
            if (yearChanged) {
                if (self.options.clickEvents.onYearChange) {
                    self.options.clickEvents.onYearChange.apply(self, [moment(self.month)]);
                }
            }
        }

        if (self.options.lengthOfTime.days || self.options.lengthOfTime.months) {
            self.render();
            // fire the today event handler regardless of whether the month changed.
            if (self.options.clickEvents.today) {
                self.options.clickEvents.today.apply(self, [moment(self.month)]);
            }
            if (self.options.clickEvents.onIntervalChange) {
                self.options.clickEvents.onIntervalChange.apply(self, [moment(self.intervalStart), moment(self.intervalEnd)]);
            }
        }
    };

    Clndr.prototype.nextYearAction = function (event) {
        var self = event.data.context;
        // before we do anything, check if there is an inactive class on the month control.
        // if it does, we want to return and take no action.
        if (self.element.find('.' + self.options.targets.nextYearButton).hasClass('inactive')) {
            return;
        }

        self.month.add(1, 'years');
        self.intervalStart.add(1, 'years');
        self.intervalEnd.add(1, 'years');

        self.render();

        if (self.options.clickEvents.nextYear) {
            self.options.clickEvents.nextYear.apply(self, [moment(self.month)]);
        }
        if (self.options.lengthOfTime.days || self.options.lengthOfTime.months) {
            if (self.options.clickEvents.onIntervalChange) {
                self.options.clickEvents.onIntervalChange.apply(self, [moment(self.intervalStart), moment(self.intervalEnd)]);
            }
        } else {
            if (self.options.clickEvents.onMonthChange) {
                self.options.clickEvents.onMonthChange.apply(self, [moment(self.month)]);
            }
            if (self.options.clickEvents.onYearChange) {
                self.options.clickEvents.onYearChange.apply(self, [moment(self.month)]);
            }
        }
    };

    Clndr.prototype.previousYearAction = function (event) {
        var self = event.data.context;
        // before we do anything, check if there is an inactive class on the month control.
        // if it does, we want to return and take no action.
        if (self.element.find('.' + self.options.targets.previousYearButton).hasClass('inactive')) {
            return;
        }

        self.month.subtract(1, 'years');
        self.intervalStart.subtract(1, 'years');
        self.intervalEnd.subtract(1, 'years');

        self.render();

        if (self.options.clickEvents.previousYear) {
            self.options.clickEvents.previousYear.apply(self, [moment(self.month)]);
        }
        if (self.options.lengthOfTime.days || self.options.lengthOfTime.months) {
            if (self.options.clickEvents.onIntervalChange) {
                self.options.clickEvents.onIntervalChange.apply(self, [moment(self.intervalStart), moment(self.intervalEnd)]);
            }
        } else {
            if (self.options.clickEvents.onMonthChange) {
                self.options.clickEvents.onMonthChange.apply(self, [moment(self.month)]);
            }
            if (self.options.clickEvents.onYearChange) {
                self.options.clickEvents.onYearChange.apply(self, [moment(self.month)]);
            }
        }
    };

    Clndr.prototype.forward = function (options) {
        if (!this.options.lengthOfTime.days) {
            // shift the interval by a month (or several months)
            this.intervalStart.add(this.options.lengthOfTime.interval, 'months').startOf('month');
            this.intervalEnd = this.intervalStart.clone().add(this.options.lengthOfTime.months || this.options.lengthOfTime.interval, 'months').subtract(1, 'days').endOf('month');
            this.month = this.intervalStart.clone();
        } else {
            // shift the interval in days
            this.intervalStart.add(this.options.lengthOfTime.interval, 'days').startOf('day');
            this.intervalEnd = this.intervalStart.clone().add(this.options.lengthOfTime.days - 1, 'days').endOf('day');
            this.month = this.intervalStart.clone();
        }

        this.render();

        if (options && options.withCallbacks) {
            if (this.options.lengthOfTime.days || this.options.lengthOfTime.months) {
                if (this.options.clickEvents.onIntervalChange) {
                    this.options.clickEvents.onIntervalChange.apply(this, [moment(this.intervalStart), moment(this.intervalEnd)]);
                }
            } else {
                if (this.options.clickEvents.onMonthChange) {
                    this.options.clickEvents.onMonthChange.apply(this, [moment(this.month)]);
                }
                // We entered a new year
                if (this.month.month() === 0 && this.options.clickEvents.onYearChange) {
                    this.options.clickEvents.onYearChange.apply(this, [moment(this.month)]);
                }
            }
        }

        return this;
    }

    Clndr.prototype.back = function (options) {
        if (!this.options.lengthOfTime.days) {
            // shift the interval by a month (or several months)
            this.intervalStart.subtract(this.options.lengthOfTime.interval, 'months').startOf('month');
            this.intervalEnd = this.intervalStart.clone().add(this.options.lengthOfTime.months || this.options.lengthOfTime.interval, 'months').subtract(1, 'days').endOf('month');
            this.month = this.intervalStart.clone();
        } else {
            // shift the interval in days
            this.intervalStart.subtract(this.options.lengthOfTime.interval, 'days').startOf('day');
            this.intervalEnd = this.intervalStart.clone().add(this.options.lengthOfTime.days - 1, 'days').endOf('day');
            this.month = this.intervalStart.clone();
        }

        this.render();

        if (options && options.withCallbacks) {
            if (this.options.lengthOfTime.days || this.options.lengthOfTime.months) {
                if (this.options.clickEvents.onIntervalChange) {
                    this.options.clickEvents.onIntervalChange.apply(this, [moment(this.intervalStart), moment(this.intervalEnd)]);
                }
            } else {
                if (this.options.clickEvents.onMonthChange) {
                    this.options.clickEvents.onMonthChange.apply(this, [moment(this.month)]);
                }
                // We went all the way back to previous year
                if (this.month.month() === 11 && this.options.clickEvents.onYearChange) {
                    this.options.clickEvents.onYearChange.apply(this, [moment(this.month)]);
                }
            }
        }

        return this;
    }

    // alternate names for convenience
    Clndr.prototype.next = function (options) {
        this.forward(options);
        return this;
    }

    Clndr.prototype.previous = function (options) {
        this.back(options);
        return this;
    }

    Clndr.prototype.setMonth = function (newMonth, options) {
        // accepts 0 - 11 or a full/partial month name e.g. "Jan", "February", "Mar"
        if (!this.options.lengthOfTime.days && !this.options.lengthOfTime.months) {
            this.month.month(newMonth);
            this.intervalStart = this.month.clone().startOf('month');
            this.intervalEnd = this.intervalStart.clone().endOf('month');
            this.render();
            if (options && options.withCallbacks) {
                if (this.options.clickEvents.onMonthChange) {
                    this.options.clickEvents.onMonthChange.apply(this, [moment(this.month)]);
                }
            }
        } else {
            console.log('You are using a custom date interval; use Clndr.setIntervalStart(startDate) instead.');
        }
        return this;
    }

    Clndr.prototype.setIntervalStart = function (newDate, options) {
        // accepts a date string or moment object
        if (this.options.lengthOfTime.days) {
            this.intervalStart = moment(newDate).startOf('day');
            this.intervalEnd = this.intervalStart.clone().add(this.options.lengthOfTime.days - 1, 'days').endOf('day');
        } else if (this.options.lengthOfTime.months) {
            this.intervalStart = moment(newDate).startOf('month');
            this.intervalEnd = this.intervalStart.clone().add(this.options.lengthOfTime.months || this.options.lengthOfTime.interval, 'months').subtract(1, 'days').endOf('month');
            this.month = this.intervalStart.clone();
        }

        if (this.options.lengthOfTime.days || this.options.lengthOfTime.months) {
            this.render();

            if (options && options.withCallbacks) {
                if (this.options.clickEvents.onIntervalChange) {
                    this.options.clickEvents.onIntervalChange.apply(this, [moment(this.intervalStart), moment(this.intervalEnd)]);
                }
            }
        } else {
            console.log('You are using a custom date interval; use Clndr.setIntervalStart(startDate) instead.');
        }
        return this;
    }

    Clndr.prototype.nextYear = function (options) {
        this.month.add(1, 'year');
        this.intervalStart.add(1, 'year');
        this.intervalEnd.add(1, 'year');
        this.render();
        if (options && options.withCallbacks) {
            if (this.options.clickEvents.onYearChange) {
                this.options.clickEvents.onYearChange.apply(this, [moment(this.month)]);
            }
            if (this.options.clickEvents.onIntervalChange) {
                this.options.clickEvents.onIntervalChange.apply(this, [moment(this.intervalStart), moment(this.intervalEnd)]);
            }
        }
        return this;
    }

    Clndr.prototype.previousYear = function (options) {
        this.month.subtract(1, 'year');
        this.intervalStart.subtract(1, 'year');
        this.intervalEnd.subtract(1, 'year');
        this.render();
        if (options && options.withCallbacks) {
            if (this.options.clickEvents.onYearChange) {
                this.options.clickEvents.onYearChange.apply(this, [moment(this.month)]);
            }
            if (this.options.clickEvents.onIntervalChange) {
                this.options.clickEvents.onIntervalChange.apply(this, [moment(this.intervalStart), moment(this.intervalEnd)]);
            }
        }
        return this;
    }

    Clndr.prototype.setYear = function (newYear, options) {
        this.month.year(newYear);
        this.intervalStart.year(newYear);
        this.intervalEnd.year(newYear);
        this.render();
        if (options && options.withCallbacks) {
            if (this.options.clickEvents.onYearChange) {
                this.options.clickEvents.onYearChange.apply(this, [moment(this.month)]);
            }
            if (this.options.clickEvents.onIntervalChange) {
                this.options.clickEvents.onIntervalChange.apply(this, [moment(this.intervalStart), moment(this.intervalEnd)]);
            }
        }
        return this;
    }

    Clndr.prototype.setEvents = function (events) {
        // go through each event and add a moment object
        if (this.options.multiDayEvents) {
            this.options.events = this.addMultiDayMomentObjectsToEvents(events);
        } else {
            this.options.events = this.addMomentObjectToEvents(events);
        }

        this.render();
        return this;
    };

    Clndr.prototype.addEvents = function (events) {
        // go through each event and add a moment object
        if (this.options.multiDayEvents) {
            this.options.events = $.merge(this.options.events, this.addMultiDayMomentObjectsToEvents(events));
        } else {
            this.options.events = $.merge(this.options.events, this.addMomentObjectToEvents(events));
        }

        this.render();
        return this;
    };

    Clndr.prototype.removeEvents = function (matchingFunction) {
        for (var i = this.options.events.length - 1; i >= 0; i--) {
            if (matchingFunction(this.options.events[i]) == true) {
                this.options.events.splice(i, 1);
            }
        }

        this.render();
        return this;
    };

    Clndr.prototype.addMomentObjectToEvents = function (events) {
        var self = this;
        var i = 0, l = events.length;
        for (i; i < l; i++) {
            // add the date as both start and end, since it's a single-day event by default
            events[i]._clndrStartDateObject = moment(events[i][self.options.dateParameter]);
            events[i]._clndrEndDateObject = moment(events[i][self.options.dateParameter]);
        }
        return events;
    }

    Clndr.prototype.addMultiDayMomentObjectsToEvents = function (events) {
        var self = this;
        var i = 0, l = events.length;
        for (i; i < l; i++) {
            // if we don't find the startDate OR endDate fields, look for singleDay
            if (!events[i][self.options.multiDayEvents.endDate] && !events[i][self.options.multiDayEvents.startDate]) {
                events[i]._clndrEndDateObject = moment(events[i][self.options.multiDayEvents.singleDay]);
                events[i]._clndrStartDateObject = moment(events[i][self.options.multiDayEvents.singleDay]);
            } else {
                // otherwise use startDate and endDate, or whichever one is present.
                events[i]._clndrEndDateObject = moment(events[i][self.options.multiDayEvents.endDate] || events[i][self.options.multiDayEvents.startDate]);
                events[i]._clndrStartDateObject = moment(events[i][self.options.multiDayEvents.startDate] || events[i][self.options.multiDayEvents.endDate]);
            }
        }
        return events;
    }

    Clndr.prototype.calendarDay = function (options) {
        var defaults = { day: "", classes: this.options.targets.empty, events: [], date: null };
        return $.extend({}, defaults, options);
    }

    Clndr.prototype.destroy = function () {
        var $container = $(this.calendarContainer);
        $container.parent().data('plugin_clndr', null);
        this.options = defaults;
        $container.empty().remove();
        this.element = null;
    };

    $.fn.clndr = function (options) {
        if (this.length === 1) {
            if (!this.data('plugin_clndr')) {
                var clndr_instance = new Clndr(this, options);
                this.data('plugin_clndr', clndr_instance);
                return clndr_instance;
            }
            return this.data('plugin_clndr');
        } else if (this.length > 1) {
            throw new Error("CLNDR does not support multiple elements yet. Make sure your clndr selector returns only one element.");
        }
    }

}));

$(function () {
        // dashboard init functions
        altair_dashboard.init();
});

    altair_dashboard = {
        init: function () {
            'use strict';
    
            altair_dashboard.clndr_calendar();
        },
        

        clndr_calendar: function () {


            clndrEvents = [
     { date: '2016-09-08', title: 'Doctors appointment', url: 'javascript:void(0)', timeStart: '10:00', timeEnd: '11:00' },
     { date: '2016-09-09', title: 'John\'s Birthday', url: 'javascript:void(0)' },
     { date: '2016-09-09', title: 'Party', url: 'javascript:void(0)', timeStart: '08:00', timeEnd: '08:30' },
     { date: '2016-09-13', title: 'Meeting', url: 'javascript:void(0)', timeStart: '18:00', timeEnd: '18:20' },
     { date: '2016-09-18', title: 'Work Out', url: 'javascript:void(0)', timeStart: '07:00', timeEnd: '08:00' },
     { date: '2016-09-18', title: 'Business Meeting', url: 'javascript:void(0)', timeStart: '11:10', timeEnd: '11:45' },
     { date: '2016-09-23', title: 'Meeting', url: 'javascript:void(0)', timeStart: '20:25', timeEnd: '20:50' },    
     { date: '2016-09-13', title: 'Business Meeting', url: 'javascript:void(0)', timeStart: '10:00', timeEnd: '11:00' }
            ]

            var $clndr_events = $('#clndr_events');

            if ($clndr_events.length) {

                var calendar_template = $('#clndr_events_template'),
                    template = calendar_template.html(),
                    template_compiled = Handlebars.compile(template);

                var daysOfTheWeek = [];

                for (var i = 0; i < 7; i++) {
                    daysOfTheWeek.push(moment().weekday(i).format('dd'));
                }
                theCalendar = $clndr_events.clndr({
                    weekOffset: 1, // Monday
                    daysOfTheWeek: daysOfTheWeek,
                    events: clndrEvents,
                    render: function (data) {
                        return template_compiled(data);
                    },
                    clickEvents: {
                        click: function (target) {
                            if (target.events.length) {

                                var $clndr_events_panel = $('.clndr_events'),
                                    thisDate = target.date._i;

                                $(target.element)
                                    .siblings('.day').removeClass('day-active')
                                    .end()
                                    .addClass('day-active');

                                if ($clndr_events_panel.children('[data-clndr-event=' + thisDate + ']').length) {

                                    $clndr_events_panel
                                        .children('.clndr_event')
                                        .hide();

                                    if (!$clndr_events.hasClass('events_visible')) {
                                        // adjust events panel
                                        dayWidthCheck();
                                        $clndr_events.addClass('events_visible');
                                        $('[data-clndr-event="' + thisDate + '"]').show();
                                        //$clndr_events_panel
                                        //    .children('[data-clndr-event=' + thisDate + ']').velocity("transition.slideUpIn", {
                                        //        stagger: 100,
                                        //        drag: true,
                                        //        delay: 280
                                        //    });
                                    } else {
                                        $('[data-clndr-event="' + thisDate + '"]').show();
                                        //$clndr_events_panel
                                        //    .children('[data-clndr-event=' + thisDate + ']').velocity("transition.slideUpIn", {
                                        //        stagger: 100,
                                        //        drag: true
                                        //    });
                                    }
                                } else if ($(target.element).hasClass('last-month')) {
                                    setTimeout(function () {
                                        $clndr_events.find('.calendar-day-' + target.date._i).click()
                                    }, 380);
                                    $clndr_events.find('.clndr_previous').click();
                                } else if ($(target.element).hasClass('next-month')) {
                                    setTimeout(function () {
                                        $clndr_events.find('.calendar-day-' + target.date._i).click()
                                    }, 380);
                                    $clndr_events.find('.clndr_next').click();
                                }
                            }
                        }
                    }
                });

                var animate_change = function () {
                    $clndr_events
                        .addClass('animated_change')
                        .removeClass('events_visible');
                    setTimeout(function () {
                        $clndr_events
                            .removeClass('animated_change')
                        ;
                    }, 380);
                };

                // next month
                $clndr_events.on('click', '.clndr_next', function (e) {
                    e.preventDefault();

                    animate_change();
                    setTimeout(function () {
                        theCalendar.forward();
                    }, 280);
                });

                // previous month
                $clndr_events.on('click', '.clndr_previous', function (e) {
                    e.preventDefault();

                    animate_change();
                    setTimeout(function () {
                        theCalendar.back();
                    }, 280);
                });

                // today
                $clndr_events.on('click', '.clndr_today', function (e) {
                    e.preventDefault();

                    animate_change();
                    setTimeout(function () {
                        theCalendar
                            .setYear(moment().format('YYYY'))
                            .setMonth(moment().format('M') - 1);
                    }, 280);

                });

                // close events
                $clndr_events.on('click', '.clndr_events_close_button', function () {
                    $clndr_events.removeClass('events_visible events_over')
                });

                // add event modal
                //event_modal = UIkit.modal("#modal_clndr_new_event");
                //$clndr_events.on('click', '.clndr_add_event', function () {
                //    if (event_modal.isActive()) {
                //        event_modal.hide();
                //    } else {
                //        event_modal.show();
                //        // hide events panel
                //        $clndr_events.removeClass('events_visible');
                //        setTimeout(function () {
                //            $window.resize();
                //        }, 280)
                //    }
                //});

                // add events submit
                $('#clndr_new_event_submit').on('click', function () {

                    var e_title = '#clndr_event_title_control',
                        e_link = '#clndr_event_link_control',
                        e_date = '#clndr_event_date_control',
                        e_start = '#clndr_event_start_control',
                        e_end = '#clndr_event_end_control';

                    if ($(e_title).val() == '') {
                        $(e_title).addClass('md-input-danger').focus();
                        altair_md.update_input($(e_title));
                        return false;
                    }

                    if ($(e_date).val() == '') {
                        $(e_date).addClass('md-input-danger').focus();
                        altair_md.update_input($(e_date));
                        return false;
                    }

                    var new_event = [
                        { date: $(e_date).val(), title: $(e_title).val(), url: $(e_link).val() ? $(e_link).val() : 'javascript:void(0)', timeStart: $(e_start).val(), timeEnd: $(e_end).val() }
                    ];

                    theCalendar.addEvents(new_event);
                    theCalendar.setMonth(moment($(e_date).val()).format('M') - 1);
                    // hide modal
                    event_modal.hide();

                    $(e_title + ',' + e_link + ',' + e_date + ',' + e_start + ',' + e_end).removeClass('md-input-danger').val('');
                    altair_md.update_input($(e_title + ',' + e_link + ',' + e_date + ',' + e_start + ',' + e_end));

                });


                var dayWidth = $clndr_events.find('.day > span').outerWidth(),
                    calMinWidth = dayWidth * 7 + 240 + 32 + 14; // day + events container + padding-left/padding-right + day padding (7*2px)

                function dayWidthCheck() {
                    ($clndr_events.width() < (calMinWidth)) ? $clndr_events.addClass('events_over') : $clndr_events.removeClass('events_over');
                }

                dayWidthCheck();

                // resize map on window resize event
                $(window).on('debouncedresize', function () {
                    dayWidthCheck();
                })
            }

        },
        // video player

    };


    